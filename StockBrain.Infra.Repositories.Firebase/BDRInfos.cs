using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class BDRInfos : BaseFirebaseRepository<BDRInfo, BDRInfo>, IBDRInfos
{
	public BDRInfos(Context context, DataBaseClient client)
		: base(context, client, "bdrInfos")
	{
	}

	public BDRInfo ByTicker(string ticker) => All().FirstOrDefault(s => s.Ticker == ticker);
	protected override BDRInfo FromDTO(BDRInfo dto) => dto;
	protected override IEnumerable<BDRInfo> FromDTO(IEnumerable<BDRInfo> dtos) => dtos;
	protected override BDRInfo FromEntity(BDRInfo entity) => entity;
	protected override IEnumerable<BDRInfo> FromEntity(IEnumerable<BDRInfo> entities) => entities;
	protected override BDRInfo BeforeSave(BDRInfo entity)
	{
		entity.GUID = entity.Ticker;
		return base.BeforeSave(entity);
	}
	protected override string GenerateGUID(BDRInfo entity)
	{
		return entity.Ticker;
	}
}
