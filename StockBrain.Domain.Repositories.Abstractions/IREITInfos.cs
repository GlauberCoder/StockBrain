using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IREITInfos :  IBaseRepository<REITInfo>
{
	REITInfo ByTicker(string ticker);
}
