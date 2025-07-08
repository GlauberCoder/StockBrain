using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPortfolioAssetUpdater
{
	void UpdateQuantities(string portfolioUUID, IDictionary<string, int> newQuantities);
}
