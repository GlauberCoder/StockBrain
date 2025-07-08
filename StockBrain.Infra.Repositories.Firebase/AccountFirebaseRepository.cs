using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public abstract class AccountFirebaseRepository<TEntity, TDTO> : BaseFirebaseRepository<TEntity, TDTO>, IBaseAccountRepository<TEntity>
	where TEntity : BaseEntity
	where TDTO : BaseEntity
{
	public string BaseName { get; private set; }
	public string AccountUUID { get; private set; }
	public AccountFirebaseRepository(Context context, DBClient client, string name, bool useCache = true)
		: base(context, client, $"users/{context?.Account?.GUID}/{name}", useCache)
	{
		DBClient = client;
		AccountUUID = Context?.Account?.GUID ?? string.Empty;
		BaseName = name;
	}

	DBClient DBClient { get; }

	public void ChangeAccount(string accountUUID)
	{
		AccountUUID = accountUUID;
		Client = DBClient.GetContext<TDTO>($"users/{accountUUID}/{BaseName}");
	}
}
