using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBonds : IBaseRepository<Bond>
{
	IEnumerable<Bond> ByPortifolio(long portifolioID);
}
