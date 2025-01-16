using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Model;

namespace StockBrain.Domain.Abstractions;

public interface IInvestmentRecommender
{
	InvestmentRecommendation Recommend(Portfolio portfolio, double investment, IDictionary<AssetType, InvestmentRecommendationTypeConfig> config);
}
