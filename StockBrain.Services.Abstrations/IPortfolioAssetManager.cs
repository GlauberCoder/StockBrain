using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPortfolioAssetManager
{
	Task ConfirmMovements(IEnumerable<string> portfolioReferences, IEnumerable<string> assets, IEnumerable<string> bonds);
	void ConfirmMovements(IEnumerable<EntityReference> portfolioReferences, IEnumerable<AssetMovement> movements, IEnumerable<BondMovement> bonds);
}
