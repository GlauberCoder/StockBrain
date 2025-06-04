using StockBrain.Domain.Models;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;
using System.Diagnostics.Metrics;

namespace StockBrain.Services;

public class PriceUpdater : IPriceUpdater
{
	public IAssets Assets { get; }
	public IPriceGetter PriceGetter { get; }
	public Context Context { get; }

	public PriceUpdater(IAssets assets, IPriceGetter priceGetter, Context context)
	{
		Assets = assets;
		PriceGetter = priceGetter;
		Context = context;
	}

	public async Task Update(Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate = null, IEnumerable<string> tickersFilter = null)
	{
		var updatedAssets = new List<Asset>();
		var assets = Assets.All();
		if(tickersFilter != null &&tickersFilter.Any())
			assets = assets.Where(a => tickersFilter.Contains(a.Ticker));
		var statuses = assets.ToDictionary(a => a.Ticker, a => (IAssetInfoUpdateStatus)new AssetInfoUpdateStatus(a.Ticker));
		onUpdate?.Invoke(statuses, false);
		foreach (var asset in assets)
		{
			var status = statuses[asset.Ticker];
			Update(asset, status);
			onUpdate?.Invoke(statuses, false);
		}
		onUpdate?.Invoke(statuses, true);
	}
	IAssetInfoUpdateStatus Update(Asset asset, IAssetInfoUpdateStatus status)
	{
		try
		{
			var price = PriceGetter.Get(asset.Ticker).Result;
			asset.MarketPrice = price;
			asset.LastPriceUpdate = Context.Today;
			Assets.Save(asset);
		}
		catch (Exception ex)
		{
			status.HasError = true;
			status.ErrorMessage = ex.Message;
		}
		status.Done = true;
		return status;
	}

	public IAssetInfoUpdateStatus Update(Asset asset)
	{
		var status = new AssetInfoUpdateStatus(asset.Ticker);
		Update(asset, status);
		return status;
	}

	public IAssetInfoUpdateStatus Update(string ticker)
	{
		var asset = Assets.ByTicker(ticker);
		return Update(asset);
	}
}
