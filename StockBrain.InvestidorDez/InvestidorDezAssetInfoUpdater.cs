using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.InfoGetters;
using StockBrain.Services;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez;

public class InvestidorDezAssetInfoUpdater : IAssetInfoUpdater
{
	Context Context { get; }
	IAssets Assets { get; }
	IStockInfos Stocks { get; }
	IREITInfos REITs { get; }
	IBDRInfos BDRs { get; }

	public InvestidorDezAssetInfoUpdater(
		Context context,
		IAssets assets,
		IStockInfos stocks,
		IREITInfos reits,
		IBDRInfos bdrs
	)
	{
		Context = context;
		Assets = assets;
		Stocks = stocks;
		REITs = reits;
		BDRs = bdrs;
	}
	public async Task UpdateAll(Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate = null, IEnumerable<string> tickersFilter = null)
	{
		var assets = Assets.All().Where(a => !a.Risk);
		if (tickersFilter != null && tickersFilter.Any())
			assets = assets.Where(a => tickersFilter.Contains(a.Ticker));

		var statuses = assets.ToDictionary(a => a.Ticker, a => (IAssetInfoUpdateStatus)new AssetInfoUpdateStatus(a.Ticker));
		onUpdate?.Invoke(statuses, false);
		var client = GetClient();
		foreach (var typeGroup in assets.GroupBy(a => a.Type))
		{
			switch (typeGroup.Key)
			{
				case AssetType.Acoes:
					await UpdateInfos(assets, client, onUpdate, Stocks, new InvestidorDezStockInfoGetter(Context, client), statuses);
					break;
				case AssetType.FII:
					await UpdateInfos(assets, client, onUpdate, REITs, new InvestidorDezREITInfoGetter(Context, client), statuses);
					break;
				case AssetType.BDR:
					await UpdateInfos(assets, client, onUpdate, BDRs, new InvestidorDezBDRInfoGetter(Context, client), statuses);
					break;
				default:
					break;
			}
		}
		Assets.Save(assets);
		onUpdate?.Invoke(statuses, true);
	}
	public async Task<IAssetInfoUpdateStatus> Update(Asset asset)
	{
		var client = GetClient();
		var status = new AssetInfoUpdateStatus(asset.Ticker);
		switch (asset.Type)
		{
			case AssetType.Acoes:
				await UpdateInfo(asset, client, Stocks, new InvestidorDezStockInfoGetter(Context, client), status);
				break;
			case AssetType.FII:
				await UpdateInfo(asset, client, REITs, new InvestidorDezREITInfoGetter(Context, client), status);
				break;
			case AssetType.BDR:
				await UpdateInfo(asset, client, BDRs, new InvestidorDezBDRInfoGetter(Context, client), status);
				break;
			default:
				break;
		}
		Assets.Save(asset);
		return status;
	}
	public async Task<IAssetInfoUpdateStatus> Update(string ticker) => await Update(Assets.ByTicker(ticker));
	async Task UpdateInfos<TInfo>(IEnumerable<Asset> assets, InvestidorDezClient client, Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate, IBaseRepository<TInfo> repository, InvestidorDezAssetInfoGetter<TInfo> getter, IDictionary<string, IAssetInfoUpdateStatus> statuses)
		where TInfo : AssetInfo, new()
	{
		foreach (var asset in assets)
		{
			UpdateInfo(asset, client, repository, getter, statuses[asset.Ticker]);
			onUpdate?.Invoke(statuses, false);
		}
	}
	async Task UpdateInfo<TInfo>(Asset asset, InvestidorDezClient client, IBaseRepository<TInfo> repository, InvestidorDezAssetInfoGetter<TInfo> getter, IAssetInfoUpdateStatus status)
		where TInfo : AssetInfo, new()
	{
		try
		{
			var result = await getter.Get(asset);
			repository.Save(result);
		}
		catch (Exception ex)
		{
			status.HasError = true;
			status.ErrorMessage = ex.Message;
			throw;
		}
		status.Done = true;
		asset.LastReview = new DateOnlySpan(Context.Today, Context.Today);
				
	}
	InvestidorDezClient GetClient() => new InvestidorDezClient(Context);
}
