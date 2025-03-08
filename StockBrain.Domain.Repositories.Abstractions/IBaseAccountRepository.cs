using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBaseAccountRepository<T> : IBaseRepository<T>
{
	void ChangeAccount(string accountUUID);
}
