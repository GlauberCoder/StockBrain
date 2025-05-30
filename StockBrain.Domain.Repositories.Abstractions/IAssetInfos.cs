using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssetInfos
{
	IDictionary<string, AssetInfo> All();
	AssetInfo By(AssetType type, string ticker);
}
