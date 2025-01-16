using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolioAssetMovements : IBaseRepository<PortfolioAssetMovement>
{
	IEnumerable<PortfolioAssetMovement> ByPortfolio(long portfolioID);
}
