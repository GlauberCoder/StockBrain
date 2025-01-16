using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssetMovements : IBaseRepository<AssetMovement>
{
	IEnumerable<AssetMovement> ByAccount(long accountID);
}
