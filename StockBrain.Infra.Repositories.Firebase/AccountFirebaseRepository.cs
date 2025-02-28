using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public abstract class AccountFirebaseRepository<TEntity, TDTO> : BaseFirebaseRepository<TEntity, TDTO>, IBaseRepository<TEntity>
	where TEntity : BaseEntity
	where TDTO : BaseEntity
{
	public AccountFirebaseRepository(Context context, DBClient client, string name, bool useCache = true)
		: base(context, client, $"users/{context.Account.GUID}/{name}", useCache)
	{
	}
}
