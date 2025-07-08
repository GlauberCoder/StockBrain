using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBaseRepository<T>
{
	IEnumerable<T> All();
	IDictionary<string, T> AllAsDictionary();
	T Save(T entity);
	IEnumerable<T> Save(IEnumerable<T> entities);
	IEnumerable<T> Delete(IEnumerable<T> entities);
	void Delete(IEnumerable<string> guids);
	T ByID(string guid);
	Task<T> ByIDAsync(string guid);
}
