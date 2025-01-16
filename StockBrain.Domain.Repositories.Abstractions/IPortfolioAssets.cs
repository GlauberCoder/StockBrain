using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolioAssets : IBaseRepository<PortfolioAsset>
{
	IEnumerable<PortfolioAsset> ByPortifolio(long portifolioID);
}
