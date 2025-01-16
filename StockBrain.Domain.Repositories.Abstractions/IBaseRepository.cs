using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBaseRepository<T>
{
	IEnumerable<T> All();
	void Save(T entity);
	void Save(IEnumerable<T> entities);
	void Delete(IEnumerable<T> entities);
	void Delete(IEnumerable<string> guids);
	T ByID(long id);
}
