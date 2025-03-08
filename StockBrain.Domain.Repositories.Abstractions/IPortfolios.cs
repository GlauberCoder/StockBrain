using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolios : IBaseAccountRepository<Portfolio>
{
	Portfolio Main();
	IEnumerable<EntityReference> References();
}
