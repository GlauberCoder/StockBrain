using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public abstract class BaseFirebaseRepository<TEntity, TDTO> :  IBaseRepository<TEntity>
	where TEntity : BaseEntity
	where TDTO : BaseEntity
{
	protected Context Context { get; }
	protected string Name { get; }
	bool UseCache { get; }
	protected DBContext<TDTO> Client { get; set; }

	public BaseFirebaseRepository(Context context, DBClient client, string name, bool useCache = true)
	{
		Context = context;
		Name = name;
		UseCache = useCache;
		Client = client.GetContext<TDTO>(name);
	}

	protected abstract TEntity FromDTO(TDTO dto);
	protected abstract TDTO FromEntity(TEntity entity);
	protected abstract IEnumerable<TEntity> FromDTO(IEnumerable<TDTO> dtos);
	protected abstract IEnumerable<TDTO> FromEntity(IEnumerable<TEntity> entities);
	protected virtual IEnumerable<TDTO> AllDTO() => GetDTOs();
	IEnumerable<TDTO> GetDTOs() => Client.Get();
	public IEnumerable<TEntity> All() => FromDTO(AllDTO().ToList());
	public TEntity ByID(string guid) => FromDTO(Client.Get(guid));
	public virtual IEnumerable<TEntity> Save(IEnumerable<TEntity> entities) {
		foreach(var entity in entities)
			Save(entity);
		return entities;
	}
	public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
	{
		Delete(entities.Select(e => e.GUID));
		return entities;
	}
	public void Delete(IEnumerable<string> guids) => Client.Delete(guids);

	public TEntity Save(TEntity entity)
	{
		var isNew = entity.IsNew();
		if (isNew)
		{
			entity.GUID = Guid.NewGuid().ToString();
			BeforeCreate(entity);
		}
		BeforeSave(entity);
		var dto = FromEntity(entity);
		Save(dto, isNew);
		return entity;
	}
	public TDTO Save(TDTO dto, bool isNew)
	{
		BeforeSaveDTO(dto);
		if (isNew)
			BeforeCreateDTO(dto);
		Client.Save(dto);
		return dto;
	}
	protected virtual TEntity BeforeCreate(TEntity entity) => entity;
	protected virtual TDTO BeforeCreateDTO(TDTO dto) => dto;
	protected virtual TEntity BeforeSave(TEntity entity) => entity;
	protected virtual TDTO BeforeSaveDTO(TDTO entity) => entity;
	protected virtual string GenerateGUID(TEntity entity) => Guid.NewGuid().ToString();

	public IDictionary<string, TEntity> AllAsDictionary() => All().ToDictionary(e => e.GUID, e => e);
}
