using StockBrain.Domain.Models;

namespace StockBrain.Domain.Abstractions;

public interface IPortfolioAssetManager
{
	void ConfirmMovements(IEnumerable<Portfolio> portfolios, IEnumerable<AssetMovement> movements, IEnumerable<BondMovement> bonds);
}
