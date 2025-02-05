using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IDecisionFactors
{
	IEnumerable<DecisionFactorAnswer> GetAnswers(REITStats stats);
	IEnumerable<DecisionFactorAnswer> GetAnswers(StockStats stats);
	IEnumerable<DecisionFactorAnswer> GetAnswers(BDRStats stats);
}
