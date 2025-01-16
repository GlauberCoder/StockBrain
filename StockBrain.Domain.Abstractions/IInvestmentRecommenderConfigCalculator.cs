using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Model;
using StockBrain.Domain.Models;

namespace StockBrain.Domain.Abstractions;

public interface IInvestmentRecommenderConfigCalculator
{
	IDictionary<AssetType, InvestmentRecommendationTypeConfig> Calc(Portfolio portfolio);
	IDictionary<AssetType, InvestmentRecommendationTypeConfig> Calc(IDictionary<AssetType, InvestmentRecommendationTypeConfig> configs);
}
