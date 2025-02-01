using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IBDRInfos :  IBaseRepository<BDRInfo>
{
	BDRInfo ByTicker(string ticker);
}
