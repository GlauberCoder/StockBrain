using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IPortfolios : IBaseRepository<Portfolio>
{
	IEnumerable<Portfolio> FromCurrentAccount();
}
