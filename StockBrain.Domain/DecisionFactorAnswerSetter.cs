using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enumerations;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.EvaluationConfigs;

namespace StockBrain.Domain;

/// <summary>
/// Responsible for assigning decision factor answers to portfolio assets,
/// using specific evaluation configurations and evaluators for each asset type (Stocks, REITs, BDRs).
/// For each group of assets in the portfolio, applies the relevant decision factors
/// according to the asset type, leveraging the appropriate information and configuration.
/// The answers are calculated using specialized evaluators and assigned to each asset,
/// enabling quantitative assessment and scoring based on defined criteria.
/// </summary>
public class DecisionFactorAnswerSetter : IDecisionFactorAnswerSetter
{
	/// <summary>
	/// Gets the evaluation configuration used for stock assets.
	/// </summary>
	StockEvaluationConfig StockConfig { get; }

	/// <summary>
	/// Gets the evaluation configuration used for REIT assets.
	/// </summary>
	REITEvaluationConfig REITConfig { get; }

	/// <summary>
	/// Gets the evaluation configuration used for BDR assets.
	/// </summary>
	BDREvaluationConfig BDRConfig { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DecisionFactorAnswerSetter"/> class
	/// with the provided evaluation configurations for stocks, REITs, and BDRs.
	/// </summary>
	/// <param name="stockConfig">The evaluation configuration for stocks.</param>
	/// <param name="reitConfig">The evaluation configuration for REITs.</param>
	/// <param name="bdrConfig">The evaluation configuration for BDRs.</param>
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

	/// <summary>
	/// Assigns decision factor answers to each portfolio asset based on its type,
	/// using the appropriate evaluation configuration and asset information.
	/// </summary>
	/// <param name="assets">The collection of portfolio assets to process.</param>
	/// <param name="infos">A dictionary mapping asset tickers to their information.</param>
	/// <param name="factors">A dictionary mapping asset types to their relevant decision factors.</param>
	/// <returns>The collection of portfolio assets with updated decision factor answers.</returns>
	public IEnumerable<PortfolioAsset> Set(IEnumerable<PortfolioAsset> assets, IDictionary<string, AssetInfo> infos, IDictionary<AssetType, IEnumerable<string>> factors)
	{
		foreach (var asset in assets)
		{
			var answers = Enumerable.Empty<DecisionFactorAnswer>();
			if (infos.TryGetValue(asset.Asset.Ticker, out var info))
				answers = Get(asset, info, factors);

			asset.SetScore(answers);
		}
		return assets;
	}
	public IEnumerable<DecisionFactorAnswer> Get(PortfolioAsset asset, AssetInfo info, IDictionary<AssetType, IEnumerable<string>> factors)
	{
		switch (asset.Asset.Type)
		{
			case AssetType.Acoes:
				return GetAnswers(new StockStats(asset, (StockInfo)info, StockConfig), AssetType.Acoes, factors[AssetType.Acoes], StockDecisionFactors.DecisionFactors);
			case AssetType.FII:
				return GetAnswers(new REITStats(asset, (REITInfo)info, REITConfig), AssetType.FII, factors[AssetType.FII], REITDecisionFactors.DecisionFactors);
			case AssetType.BDR:
				return GetAnswers(new BDRStats(asset, (BDRInfo)info, BDRConfig), AssetType.BDR, factors[AssetType.BDR], BDRDecisionFactors.DecisionFactors);
			default:
				return Enumerable.Empty<DecisionFactorAnswer>();
		}
	}

	/// <summary>
	/// Gets the decision factor answers for a given asset statistics object, asset type, factors, and evaluators.
	/// </summary>
	/// <typeparam name="TStats">The type of the statistics object.</typeparam>
	/// <param name="stats">The statistics object for the asset.</param>
	/// <param name="type">The asset type.</param>
	/// <param name="factors">The relevant decision factors for the asset type.</param>
	/// <param name="evaluators">A dictionary mapping factor names to their evaluators.</param>
	/// <returns>A collection of decision factor answers for the asset.</returns>
	public IEnumerable<DecisionFactorAnswer> GetAnswers<TStats>(TStats stats, AssetType type, IEnumerable<string> factors, IDictionary<string, DecisionFactorEvaluator<TStats>> evaluators)
	{
		return factors.Select(f => evaluators[f].Answer(stats));
	}

}
