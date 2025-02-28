using Firebase.Database;
using Firebase.Database.Query;
using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Firebase.Services;

public class DBClient
{
	//https://github.com/step-up-labs/firebase-database-dotnet
	public DBClient(DataBaseConfig config)
	{
		Client = new FirebaseClient(config.Url, new FirebaseOptions
		{
			AuthTokenAsyncFactory = () => Task.FromResult(config.Auth)
		}); 
	}

	FirebaseClient Client { get; }

	public DBContext<TEntity> GetContext<TEntity>(string path) where TEntity : BaseEntity => new DBContext<TEntity>(Client.Child(path));
	public TEntity Single<TEntity>(string path)=> Client.Child(path).OnceSingleAsync<TEntity>().Result;

}
