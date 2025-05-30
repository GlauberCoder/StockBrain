using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models;

namespace StockBrain.Domain.Abstractions;

public interface IDecisionFactorAnswerSetter
{
	IEnumerable<PortfolioAsset> Set(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IDictionary<AssetType, IEnumerable<string>> factors);
	IEnumerable<DecisionFactorAnswer> Get(PortfolioAsset asset, AssetInfo info, IDictionary<AssetType, IEnumerable<string>> factors);
}
