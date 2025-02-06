using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPortfolioAssetManager
{
	void ConfirmMovements(IEnumerable<Portfolio> portfolios, IEnumerable<AssetMovement> movements, IEnumerable<BondMovement> bonds);
}
