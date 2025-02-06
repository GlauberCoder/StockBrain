using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssetInfos
{
	IDictionary<string, AssetInfo> All();
}
