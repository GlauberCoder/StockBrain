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

	public IDictionary<AssetType, IEnumerable<string>> All()
	{
		var json = File.ReadAllText(GetPath());
		return json.Deserialize<Dictionary<AssetType, IEnumerable<string>>>();

	}
	string GetPath() => Path.Combine(Config.BasePath, $"decisionFactors.json");
}
