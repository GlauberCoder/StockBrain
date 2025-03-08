using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;

namespace StockBrain.Services;

public class AssetCreator : IAssetCreator
{
	IAssetDataCreator AssetDataCreator { get; set; }
	IPriceUpdater PriceUpdater { get; }
	IAssetInfoUpdater AssetInfoUpdater { get; }

	public AssetCreator(IAssetDataCreator assetDataCreator, IPriceUpdater priceUpdater, IAssetInfoUpdater assetInfoUpdater)
	{
		AssetDataCreator = assetDataCreator;
		PriceUpdater = priceUpdater;
		AssetInfoUpdater = assetInfoUpdater;
	}
	public void CreateAndAddToPortfolio(string ticker, string name, string description, DateOnly fundation, DateOnly ipo, string negativeNotes, string positiveNotes, string sectorName, string segmentName, AssetType type)
	{
		AssetDataCreator.CreateAndAddToPortfolio(ticker, name, description, fundation, ipo, negativeNotes, positiveNotes, sectorName, segmentName, type);
		var tickers = new List<string> { ticker };
		PriceUpdater.Update(null, tickers).Wait();
		AssetInfoUpdater.UpdateAll(null, tickers).Wait();
	}
}
