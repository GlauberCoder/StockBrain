using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Model;
using StockBrain.Utils;

namespace StockBrain.Domain;

public class InvestmentRecommenderConfigCalculator : IInvestmentRecommenderConfigCalculator
{
	public IDictionary<AssetType, InvestmentRecommendationTypeConfig> Calc(Portfolio portfolio) 
	{
		return portfolio.Types.ToDictionary(p => p.Key, p => new InvestmentRecommendationTypeConfig(p.Key, p.Value.Target.Proportion));
	}
	public IDictionary<AssetType, InvestmentRecommendationTypeConfig> Calc(IDictionary<AssetType, InvestmentRecommendationTypeConfig> configs)
	{
		var total = configs.Where(c => c.Value.Invest).Sum(c => c.Value.Target);
		return configs.ToDictionary(p => p.Key, p => {
			var value = configs[p.Key];
			var newTarget = value.Invest ? (value.Target / total).ToPrecision(2) : 0;
			return new InvestmentRecommendationTypeConfig(p.Key, newTarget, value.Invest, value.Amount);
		});
	}
}
