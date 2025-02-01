using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class StockInfos : BaseJSONFIleRepository<StockInfo, StockInfo>, IStockInfos
{
	public StockInfos(Context context, DataJSONFilesConfig config)
		: base(context, config, "stockInfos")
	{
	}

	public StockInfo ByTicker(string ticker) => All().FirstOrDefault(s => s.Ticker == ticker);
	protected override StockInfo FromDTO(StockInfo dto) => dto;
	protected override IEnumerable<StockInfo> FromDTO(IEnumerable<StockInfo> dtos) => dtos;
	protected override StockInfo FromEntity(StockInfo entity) => entity;
	protected override IEnumerable<StockInfo> FromEntity(IEnumerable<StockInfo> entities) => entities;
}
