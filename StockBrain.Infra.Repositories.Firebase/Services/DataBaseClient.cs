using Firebase.Database;
using Firebase.Database.Query;
using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Firebase.Services;

public class DataBaseClient
{
	public DataBaseClient(DataBaseConfig config)
	{
		Client = new FirebaseClient(config.Url, new FirebaseOptions
		{
			AuthTokenAsyncFactory = () => Task.FromResult(config.Auth)
		}); 
	}

	FirebaseClient Client { get; }

	public TEntity GetValue<TEntity>(string path) => Client.Child(path).OnceSingleAsync<TEntity>().Result;
	public IEnumerable<TEntity> Get<TEntity>(string path) where TEntity : BaseEntity => Client.Child(path).OnceAsync<TEntity>().Result?.Select(e => e.Object) ?? Enumerable.Empty<TEntity>();
	public TEntity Get<TEntity>(string path, string guid) where TEntity : BaseEntity => Client.Child(path).Child(guid).OnceSingleAsync<TEntity>().Result;
	public TEntity Save<TEntity>(string path, TEntity entity) where TEntity : BaseEntity
	{ 
		if (entity.ID == 0 || string.IsNullOrEmpty(entity.GUID) || entity.GUID == Guid.Empty.ToString()) 
		{
			entity.ID = Get<TEntity>(path).Max(e => e.ID) + 1;
			entity.GUID = Guid.NewGuid().ToString();
		}
		Client.Child(path).Child(entity.GUID).PutAsync(entity);
		return entity;
	}
	public IEnumerable<TEntity> Save<TEntity>(string path, IEnumerable<TEntity> entities) where TEntity : BaseEntity
	{
		var results = new List<TEntity>();
		
		foreach(var entity in entities)
			results.Add(Save(path, entity));

		return results;
	}
	public TEntity Delete<TEntity>(string path, TEntity entity) where TEntity : BaseEntity
	{
		Delete(path, entity.GUID);
		return entity;
	}
	public void Delete(string path, string guid)
	{
		Client.Child(path).Child(guid).DeleteAsync();

	}
}
