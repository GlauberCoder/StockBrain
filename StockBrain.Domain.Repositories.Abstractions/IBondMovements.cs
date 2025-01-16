using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBondMovements :  IBaseRepository<BondMovement>
{
	IEnumerable<BondMovement> ByAccount(long accountID);
}
