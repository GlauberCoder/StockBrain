using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enumerations;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.EvaluationConfigs;

namespace StockBrain.Domain;

public class DecisionFactorAnswerSetter : IDecisionFactorAnswerSetter
{
	StockEvaluationConfig StockConfig { get; }
	REITEvaluationConfig REITConfig { get; }
	BDREvaluationConfig BDRConfig { get; }

	public DecisionFactorAnswerSetter(
		StockEvaluationConfig stockConfig,
		REITEvaluationConfig reitConfig,
		BDREvaluationConfig bdrConfig
	)
	{
		StockConfig = stockConfig;
		REITConfig = reitConfig;
		BDRConfig = bdrConfig;
	}

	public IEnumerable<PortfolioAsset> Set(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IDictionary<AssetType, IEnumerable<string>> factors)
	{
		foreach (var typeGroup in assets.GroupBy(a => a.Asset.Type))
		{
			switch (typeGroup.Key)
			{
				case AssetType.Acoes:
					SetStocks(typeGroup, infos, factors[typeGroup.Key]);
					break;
				case AssetType.FII:
					SetREITs(typeGroup, infos, factors[typeGroup.Key]);
					break;
				case AssetType.BDR:
					SetBDRs(typeGroup, infos, factors[typeGroup.Key]);
					break;
				default:
					break;
			}
		}
		return assets;
	}
	void SetStocks(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IEnumerable<string> factors)
	{
		foreach (var asset in assets)
		{
			var answers = Enumerable.Empty<DecisionFactorAnswer>();
			if (infos.TryGetValue(asset.Asset.Ticker, out var info))
			{
				var stats = new StockStats(asset, (StockInfo)info, StockConfig);
				answers = GetAnswers(stats, factors);
			}
			asset.SetScore(answers);
		}
	}
	void SetREITs(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IEnumerable<string> factors)
	{
		foreach (var asset in assets)
		{
			var answers = Enumerable.Empty<DecisionFactorAnswer>();
			if (infos.TryGetValue(asset.Asset.Ticker, out var info))
			{
				var stats = new REITStats(asset, (REITInfo)info, REITConfig);
				answers = GetAnswers(stats, factors);
			}
			asset.SetScore(answers);
		}
	}
	void SetBDRs(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IEnumerable<string> factors)
	{
		foreach (var asset in assets)
		{
			var answers = Enumerable.Empty<DecisionFactorAnswer>();
			if (infos.TryGetValue(asset.Asset.Ticker, out var info))
			{
				var stats = new BDRStats(asset, (BDRInfo)info, BDRConfig);
				answers = GetAnswers(stats, factors);
			}
			asset.SetScore(answers);
		}
	}

	public IEnumerable<DecisionFactorAnswer> GetAnswers(REITStats stats, IEnumerable<string> factors) => GetAnswers(stats, AssetType.FII, factors, REITDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers(StockStats stats, IEnumerable<string> factors) => GetAnswers(stats, AssetType.Acoes, factors, StockDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers(BDRStats stats, IEnumerable<string> factors) => GetAnswers(stats, AssetType.BDR, factors, BDRDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers<TStats>(TStats stats, AssetType type, IEnumerable<string> factors, IDictionary<string, DecisionFactorEvaluator<TStats>> evaluators)
	{
		return factors.Select(f => evaluators[f].Answer(stats));
	}
}
