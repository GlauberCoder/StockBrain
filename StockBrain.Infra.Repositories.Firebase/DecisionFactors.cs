using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class DecisionFactors : IDecisionFactors
{
	public DecisionFactors(Context context, DataBaseClient client)
	{
		Client = client;
	}

	DataBaseClient Client { get; }

	public IDictionary<AssetType, IEnumerable<string>> All() => Client.GetValue<IDictionary<AssetType, IEnumerable<string>>>("decisionFactors");
}
