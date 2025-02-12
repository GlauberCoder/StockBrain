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
					await UpdateStocks(typeGroup, client, onUpdate, statuses);
					break;
				case AssetType.FII:
					await UpdateREITs(typeGroup, client, onUpdate, statuses);
					break;
				case AssetType.BDR:
					await UpdateBDRs(typeGroup, client, onUpdate, statuses);
					break;
				default:
					break;
			}
		}
		Assets.Save(assets);
		onUpdate?.Invoke(statuses, true);
	}
	async Task UpdateInfos<TInfo>(IEnumerable<Asset> assets, InvestidorDezClient client, Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate, IBaseRepository<TInfo> repository, InvestidorDezAssetInfoGetter<TInfo> getter, IDictionary<string, IAssetInfoUpdateStatus> statuses)
		where TInfo : AssetInfo, new()
	{
		var infos = new List<TInfo>();
		foreach (var asset in assets)
		{
			var status = statuses[asset.Ticker];
			try
			{
				var result = await getter.Get(asset);
				infos.Add(result);
			}
			catch (Exception ex)
			{
				status.HasError = true;
				status.ErrorMessage = ex.Message;
				throw;
			}
			status.Done = true;
			asset.LastReview = new DateOnlySpan(Context.Today, Context.Today);
			onUpdate?.Invoke(statuses, false);
		}
		repository.Save(infos);
	}
	async Task UpdateStocks(IEnumerable<Asset> assets, InvestidorDezClient client, Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate, IDictionary<string, IAssetInfoUpdateStatus> statuses)
	{
		await UpdateInfos(assets, client, onUpdate, Stocks, new InvestidorDezStockInfoGetter(Context,client), statuses);
	}
	async Task UpdateREITs(IEnumerable<Asset> assets, InvestidorDezClient client, Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate, IDictionary<string, IAssetInfoUpdateStatus> statuses)
	{
		await UpdateInfos(assets, client, onUpdate, REITs, new InvestidorDezREITInfoGetter(Context, client), statuses);
	}
	async Task UpdateBDRs(IEnumerable<Asset> assets, InvestidorDezClient client, Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate, IDictionary<string, IAssetInfoUpdateStatus> statuses)
	{
		await UpdateInfos(assets, client, onUpdate, BDRs, new InvestidorDezBDRInfoGetter(Context, client), statuses);
	}
	InvestidorDezClient GetClient() => new InvestidorDezClient(Context);
}
