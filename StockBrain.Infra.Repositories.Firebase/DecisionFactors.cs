using FireSharp;
using FireSharp.Config;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.FirebaseServices;

namespace StockBrain.Infra.Repositories.Firebase;

public class DecisionFactors : IDecisionFactors
{
	public DecisionFactors(Context context, FirebaseConfigModel config)
	{
		Config = config;
	}

	FirebaseConfigModel Config { get; }

	public IDictionary<AssetType, IEnumerable<string>> All()
	{
		var client = new FirebaseClient(new FirebaseConfig
		{
			AuthSecret = Config.Secret,
			BasePath = $"{Config.BasePath}"
		});
		return client.Get("decisionFactors").ResultAs<IDictionary<AssetType, IEnumerable<string>>>();

	}
}
