using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class StockInfos : BaseFirebaseRepository<StockInfo, StockInfo>, IStockInfos
{
	public StockInfos(Context context, DataBaseClient client)
		: base(context, client, "stockInfos")
	{
	}

	public StockInfo ByTicker(string ticker) => All().FirstOrDefault(s => s.Ticker == ticker);
	protected override StockInfo FromDTO(StockInfo dto) => dto;
	protected override IEnumerable<StockInfo> FromDTO(IEnumerable<StockInfo> dtos) => dtos;
	protected override StockInfo FromEntity(StockInfo entity) => entity;
	protected override IEnumerable<StockInfo> FromEntity(IEnumerable<StockInfo> entities) => entities;
	protected override StockInfo BeforeSave(StockInfo entity)
	{
		entity.GUID = entity.Ticker;
		return base.BeforeSave(entity);
	}
	protected override string GenerateGUID(StockInfo entity)
	{
		return entity.Ticker;
	}
}
