using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class BDRInfos : BaseJSONFIleRepository<BDRInfo, BDRInfo>, IBDRInfos
{
	public BDRInfos(Context context, DataJSONFilesConfig config)
		: base(context, config, "bdrInfos")
	{
	}

	public BDRInfo ByTicker(string ticker) => All().FirstOrDefault(s => s.Ticker == ticker);
	protected override BDRInfo FromDTO(BDRInfo dto) => dto;
	protected override IEnumerable<BDRInfo> FromDTO(IEnumerable<BDRInfo> dtos) => dtos;
	protected override BDRInfo FromEntity(BDRInfo entity) => entity;
	protected override IEnumerable<BDRInfo> FromEntity(IEnumerable<BDRInfo> entities) => entities;
}
