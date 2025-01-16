using StockBrain.Domain.Models;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;

namespace StockBrain.Services;

public class PriceUpdater : IPriceUpdater
{
	public IAssets Assets { get; }
	public IPriceGetter PriceGetter { get; }
	public PriceUpdater(IAssets assets, IPriceGetter priceGetter)
	{
		Assets = assets;
		PriceGetter = priceGetter;
	}

	public async Task Update(IEnumerable<Asset> assets)
	{
		var updatedAssets = new List<Asset>();
		foreach (var asset in assets)
		{
			var price = await PriceGetter.Get(asset.Ticker);
			asset.MarketPrice = price;
			updatedAssets.Add(asset);
		}
		Assets.Save(updatedAssets);
	}

	public Task UpdateAll() => Update(Assets.All());

	public Task UpdateMissing() => Update(Assets.All().Where(a => !a.MarketPrice.HasValue));
}
