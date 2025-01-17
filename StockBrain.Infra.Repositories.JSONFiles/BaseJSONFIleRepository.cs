using StockBrain.Domain.Models;
using StockBrain.Utils;

namespace StockBrain.Infra.Repositories.JSONFiles;

public abstract class BaseJSONFIleRepository<TEntity, TDTO> 
	where TEntity : BaseEntity
	where TDTO : BaseEntity
{
	protected Context Context { get; }
	DataJSONFilesConfig Config { get; }
	string FileName { get; }

	public BaseJSONFIleRepository(Context context, DataJSONFilesConfig config, string fileName)
	{
		Context = context;
		Config = config;
		FileName = fileName;
	}
	protected abstract TEntity FromDTO(TDTO dto);
	protected abstract TDTO FromEntity(TEntity entity);
	protected abstract IEnumerable<TEntity> FromDTO(IEnumerable<TDTO> dtos);
	protected abstract IEnumerable<TDTO> FromEntity(IEnumerable<TEntity> entities);
	protected virtual IEnumerable<TDTO> AllDTO()
	{
		var json = File.ReadAllText(GetPath());
		return json.Deserialize<List<TDTO>>().OrderBy(e => e.ID);

	}
	public IEnumerable<TEntity> All()
	{
		return FromDTO(AllDTO());
	}
	public TEntity ByID(long id)
	{
		return FromDTO(AllDTO().First(a => a.ID == id));
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
		var json = entities.Select(FromEntity).Serialize();
		File.WriteAllText(GetPath(), json);
	}
	IEnumerable<TEntity> UpdateEntities(IEnumerable<TEntity> oldOnes, IEnumerable<TEntity> newOnes)
	{
		var updatedList = oldOnes.ToList();
		var id = (oldOnes.Any() ? oldOnes.Max(o => o.ID) : 0)+1;
		foreach (var entity in newOnes)
		{
			BeforeSave(entity);
			var oldEntity = updatedList.FirstOrDefault(e => e.GUID == entity.GUID);
			if (oldEntity == null)
			{
				entity.ID = id++;
				entity.GUID = Guid.NewGuid().ToString();
				BeforeCreate(entity);
				updatedList.Add(entity);
			}
			else
			{
				var index = updatedList.IndexOf(oldEntity);
				updatedList[index] = entity;
			}
		}
		return updatedList;
	}
	string GetPath() => Path.Combine(Config.BasePath, $"{FileName}.json");
	protected virtual TEntity BeforeCreate(TEntity entity) => entity;
	protected virtual TEntity BeforeSave(TEntity entity) => entity;
}
