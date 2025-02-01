using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Services.Abstrations;

public interface IAssetInfoGetter<TInfo>
{
	Task<TInfo> Get(Asset asset);
	Task<IEnumerable<TInfo>> Get(IEnumerable<Asset> assets);

}
