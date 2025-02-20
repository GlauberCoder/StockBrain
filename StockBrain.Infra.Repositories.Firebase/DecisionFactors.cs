using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Firebase;

public class DecisionFactors : IDecisionFactors
{
	public DecisionFactors(Context context, IFirebaseClient client)
	{
		Client = client;
	}

	IFirebaseClient Client { get; }

	public IDictionary<AssetType, IEnumerable<string>> All()
	{
		return Client.Get("decisionFactors").ResultAs<IDictionary<AssetType, IEnumerable<string>>>();

	}
}
