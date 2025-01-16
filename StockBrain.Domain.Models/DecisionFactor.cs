using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class DecisionFactor : BaseEntity
{
	public string Name { get; init; }
	public string Description { get; init; }
	public AssetType Type { get; init; }
	public DecisionFactorAnswerStrategy Strategy { get; init; }

	public int Points(PortfolioAsset asset) 
	{
		switch (Strategy)
		{
			case DecisionFactorAnswerStrategy.CurrentPriceLowerThanAvaragePrice:
				return asset.AveragePrice < asset.Asset.MarketPrice ? 1 : -1;
			case DecisionFactorAnswerStrategy.IPOOver5Years:
				return asset.Asset.IPO.Span.Years() >= 5 ? 1 : -1; ;
			case DecisionFactorAnswerStrategy.FoundedOver10Years:
				return asset.Asset.Foundation.Span.Years() >= 5 ? 1 : -1;
			case DecisionFactorAnswerStrategy.Manual:
			default:
				return 0;
		}
	}
}
