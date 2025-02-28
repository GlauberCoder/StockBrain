using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolios : IBaseRepository<Portfolio>
{
	Portfolio Main();
	IEnumerable<EntityReference> References();
}
