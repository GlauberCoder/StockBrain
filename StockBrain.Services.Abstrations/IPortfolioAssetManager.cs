using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPortfolioAssetManager
{
	void ConfirmMovements(IEnumerable<EntityReference> portfolioReferences, IEnumerable<AssetMovement> movements, IEnumerable<BondMovement> bonds);
}
