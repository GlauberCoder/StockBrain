using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class DecisionFactor : BaseEntity
{
	public string Name { get; init; }
	public string Description { get; init; }
	public bool IsPortfolio => Strategy == DecisionFactorAnswerStrategy.CurrentPriceLowerThanAvaragePrice;
	public AssetType Type { get; init; }
	public DecisionFactorAnswerStrategy Strategy { get; init; }

	public int Points(PortfolioAsset asset) 
	{
		if(Strategy == DecisionFactorAnswerStrategy.CurrentPriceLowerThanAvaragePrice)
			return asset.AveragePrice < asset.Asset.MarketPrice ? 1 : -1;
		return Points(asset.Asset, asset.Asset.Factors.FirstOrDefault(a => a.Factor.ID == ID));
	}
	public int Points(Asset asset, AssetDecisionFactor factor)
	{
		if (factor == null) return 0;
		switch (Strategy)
		{
			case DecisionFactorAnswerStrategy.IPOOver5Years:
				return asset.IPO.Span.Years() >= 5 ? 1 : -1; ;
			case DecisionFactorAnswerStrategy.FoundedOver10Years:
				return asset.Foundation.Span.Years() >= 5 ? 1 : -1;
			case DecisionFactorAnswerStrategy.Manual:
			case DecisionFactorAnswerStrategy.FromScrap:
				return factor.Answer.HasValue ? factor.Answer.Value ? 1 : -1 : 0;
			default:
				return 0;
		}
	}
}
