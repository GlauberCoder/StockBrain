using Firebase.Database.Query;
using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Firebase.Services;

public class DBContext<TEntity>
	where TEntity : BaseEntity
{
	public DBContext(ChildQuery node)
	{
		Node = node;
	}

	ChildQuery Node { get; }

	public IEnumerable<TEntity> GetShallow()
	{
		return Node.Shallow()
					.OnceAsync<TEntity>()
					.Result?
					.Select(e => e.Object) ?? Enumerable.Empty<TEntity>();
	}
	public IEnumerable<TEntity> Get()
	{
		return Node
					.OnceAsync<TEntity>()
					.Result?
					.Select(e => e.Object) ?? Enumerable.Empty<TEntity>();
	}
	public TEntity Get(string guid) => Node.Child(guid).OnceSingleAsync<TEntity>().Result;
	public TEntity Save(TEntity entity) 
	{
		if (entity.IsNew())
			entity.GUID = Guid.NewGuid().ToString();

		Node.Child(entity.GUID).PutAsync(entity);
		return entity;
	}
	public IEnumerable<TEntity> Save(IEnumerable<TEntity> entities) 
	{
		var results = new List<TEntity>();

		foreach (var entity in entities)
			results.Add(Save(entity));

		return results;
	}
	public TEntity Delete(TEntity entity) 
	{
		Delete(entity.GUID);
		return entity;
	}
	public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities) {
		Delete(entities.Select(e => e.GUID));
		return entities;
	}
	public void Delete(IEnumerable<string> guids)
	{
		foreach (var guid in guids)
			Delete(guid);	
	}
	public void Delete(string guid) => Node.Child(guid).DeleteAsync().Wait();
}
