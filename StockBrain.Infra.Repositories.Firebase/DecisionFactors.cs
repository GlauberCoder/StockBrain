using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class DecisionFactors : IDecisionFactors
{
	public DecisionFactors(Context context, DBClient client)
	{
		Client = client;
	}

	DBClient Client { get; }

	public IDictionary<AssetType, IEnumerable<string>> All() => Client.Single<IDictionary<AssetType, IEnumerable<string>>>("decisionFactors");
}
