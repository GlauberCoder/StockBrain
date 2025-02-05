using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enumerations;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Utils;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class DecisionFactors : IDecisionFactors
{
	public DecisionFactors(Context context, DataJSONFilesConfig config)
	{
		Config = config;
	}

	public DataJSONFilesConfig Config { get; }

	public IEnumerable<DecisionFactorAnswer> GetAnswers(REITStats stats) => GetAnswers(stats, AssetType.FII, REITDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers(StockStats stats) => GetAnswers(stats, AssetType.Acoes, StockDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers(BDRStats stats) => GetAnswers(stats, AssetType.BDR, BDRDecisionFactors.DecisionFactors);
	public IEnumerable<DecisionFactorAnswer> GetAnswers<TStats>(TStats stats, AssetType type, IDictionary<string, DecisionFactorEvaluator<TStats>> evaluators)
	{
		var factors = GetFactors()[type];
		return factors.Select(f => evaluators[f].Answer(stats));
	}

	protected virtual IDictionary<AssetType, IEnumerable<string>> GetFactors()
	{
		var json = File.ReadAllText(GetPath());
		return json.Deserialize<Dictionary<AssetType, IEnumerable<string>>>();

	}
	string GetPath() => Path.Combine(Config.BasePath, $"decisionFactors.json");
}
