using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IStockInfos :  IBaseRepository<StockInfo>
{
	StockInfo ByTicker(string ticker);
}
