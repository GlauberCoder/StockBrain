using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolioAssetBrokers :  IBaseRepository<PortfolioAssetBroker>
{
	IEnumerable<PortfolioAssetBroker> ByPortfolio(long portfolioID);
}
