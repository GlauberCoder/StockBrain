using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models;

namespace StockBrain.Domain.Abstractions;

public interface IDecisionFactorAnswerSetter
{
	void Set(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IDictionary<AssetType, IEnumerable<string>> factors);
}
