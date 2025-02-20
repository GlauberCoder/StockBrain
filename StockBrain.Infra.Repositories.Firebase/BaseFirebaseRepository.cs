using FireSharp.Interfaces;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public abstract class BaseFirebaseRepository<TEntity, TDTO>
	where TEntity : BaseEntity
	where TDTO : BaseEntity
{
	protected Context Context { get; }
	string Name { get; }
	bool UseCache { get; }
	IFirebaseClient Client { get; }

	public BaseFirebaseRepository(Context context, IFirebaseClient client, string name, bool useCache = true)
	{
		Context = context;
		Name = name;
		UseCache = useCache;
		Client = client;
	}
	protected abstract TEntity FromDTO(TDTO dto);
	protected abstract TDTO FromEntity(TEntity entity);
	protected abstract IEnumerable<TEntity> FromDTO(IEnumerable<TDTO> dtos);
	protected abstract IEnumerable<TDTO> FromEntity(IEnumerable<TEntity> entities);
	protected virtual IEnumerable<TDTO> AllDTO()
	{
		return UseCache ? MemoryCacheService.GetOrAdd(Name, GetDTOs) : GetDTOs();
	}
	IEnumerable<TDTO> GetDTOs()
	{
		var result = Client.Get(Name).ResultAs<IDictionary<string, TDTO>>();
		return result?.Select(s => s.Value).ToList() ?? Enumerable.Empty<TDTO>();

	}
	public IEnumerable<TEntity> All()
	{
		return FromDTO(AllDTO().ToList());
	}
	public TEntity ByID(long id)
	{
		return FromDTO(AllDTO().ToList().First(a => a.ID == id));
	}
	public void Save(TEntity entity)
	{

		DeleteOldAndSave(UpdateEntities(All(), new List<TEntity> { entity }));
	}
	public virtual void Save(IEnumerable<TEntity> entities)
	{
		DeleteOldAndSave(UpdateEntities(All(), entities));
	}
	public void Delete(IEnumerable<TEntity> entities)
	{
		Delete(entities.Select(e => e.GUID));
	}
	public void Delete(IEnumerable<string> guids)
	{
		var entities = All().Where(e => !guids.Contains(e.GUID)).ToList();

		DeleteOldAndSave(entities);
	}
	void DeleteOldAndSave(IEnumerable<TEntity> entities)
	{
		Client.Set(Name, entities.Select(FromEntity).ToDictionary(e => e.GUID, e => e));
	}
	IEnumerable<TEntity> UpdateEntities(IEnumerable<TEntity> oldOnes, IEnumerable<TEntity> newOnes)
	{
		var updatedList = oldOnes.ToList();
		var id = (oldOnes.Any() ? oldOnes.Max(o => o.ID) : 0) + 1;
		foreach (var entity in newOnes)
		{
			BeforeSave(entity);
			var oldEntity = updatedList.FirstOrDefault(e => e.GUID == entity.GUID);
			if (oldEntity == null)
			{
				entity.ID = id++;
				entity.GUID = GenerateGUID(entity);
				BeforeCreate(entity);
				updatedList.Add(entity);
			}
			else
			{
				var index = updatedList.IndexOf(oldEntity);
				entity.ID = oldEntity.ID;
				updatedList[index] = entity;
			}
		}
		return updatedList;
	}
	protected virtual TEntity BeforeCreate(TEntity entity) => entity;
	protected virtual TEntity BeforeSave(TEntity entity) => entity;
	protected virtual string GenerateGUID(TEntity entity) => Guid.NewGuid().ToString();
}
