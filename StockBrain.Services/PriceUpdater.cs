using StockBrain.Domain.Models;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;

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

	public async Task Update(IEnumerable<Asset> assets, Action<IEnumerable<Asset>> onFinish)
	{
		var updatedAssets = new List<Asset>();
		foreach (var asset in assets)
		{
			var price = await PriceGetter.Get(asset.Ticker);
			asset.MarketPrice = price;
			asset.LastPriceUpdate = Context.Today;
			updatedAssets.Add(asset);
		}
		Assets.Save(updatedAssets);
		onFinish(updatedAssets);
	}

	public Task UpdateAll(Action<IEnumerable<Asset>> onFinish) => Update(Assets.All(), onFinish);

	public Task UpdateMissing(Action<IEnumerable<Asset>> onFinish) => Update(Assets.All().Where(a => !a.MarketPrice.HasValue), onFinish);
}
