using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class REITInfos : BaseJSONFIleRepository<REITInfo, REITInfo>, IREITInfos
{
	public REITInfos(Context context, DataJSONFilesConfig config)
		: base(context, config, "reitInfos")
	{
	}

	public REITInfo ByTicker(string ticker) => All().FirstOrDefault(s => s.Ticker == ticker);
	protected override REITInfo FromDTO(REITInfo dto) => dto;
	protected override IEnumerable<REITInfo> FromDTO(IEnumerable<REITInfo> dtos) => dtos;
	protected override REITInfo FromEntity(REITInfo entity) => entity;
	protected override IEnumerable<REITInfo> FromEntity(IEnumerable<REITInfo> entities) => entities;
	protected override REITInfo BeforeSave(REITInfo entity)
	{
		entity.GUID = entity.Ticker;
		return base.BeforeSave(entity);
	}
	protected override string GenerateGUID(REITInfo entity)
	{
		return entity.Ticker;
	}
}
