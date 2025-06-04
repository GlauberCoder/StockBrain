using StockBrain.Domain.Models;
using StockBrain.DTOs;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssetMovements : IBaseAccountRepository<AssetMovement>
{
	void Add(AssetMovementDTO assetMovement);
	void DefineBroker(string brokerUUID, IEnumerable<string> uuids);
	void Clear();
}
