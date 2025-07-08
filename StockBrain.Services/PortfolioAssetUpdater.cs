using StockBrain.Domain.Models;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;

namespace StockBrain.Services;

public class PortfolioAssetUpdater : IPortfolioAssetUpdater
{
	public IPortfolios Portfolios { get; }

	public PortfolioAssetUpdater(IPortfolios portfolios)
	{
		Portfolios = portfolios;
	}
	public void UpdateQuantities(string portfolioUUID, IDictionary<string, int> newQuantities)
	{ 
		var porfolio = Portfolios.ByID(portfolioUUID);
		foreach (var newQuantity in newQuantities) 
		{
			var asset = porfolio.Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == newQuantity.Key);
			if (asset != null)
			{
				asset.Asset.Quantity = newQuantity.Value;
				if(asset.Asset.Brokers.Any())
					asset.Asset.Brokers.First().Quantity = newQuantity.Value;
			}

		}
		Portfolios.Save(porfolio);
	}
}
