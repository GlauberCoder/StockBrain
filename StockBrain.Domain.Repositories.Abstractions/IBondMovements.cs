using StockBrain.Domain.Models;
using StockBrain.DTOs;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBondMovements : IBaseAccountRepository<BondMovement>
{
	void Add(BondMovementDTO bondMovement);
	void DefineBroker(string brokerUUID, IEnumerable<string> uuids);
	void Clear();
}
