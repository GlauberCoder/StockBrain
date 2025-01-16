using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class AssetDecisionFactor : BaseEntity
{
	public long AssetID { get; init; }
	public DecisionFactor Factor { get; init; }
	public bool? Answer { get; set; }
	public int Points(PortfolioAsset asset) 
	{
		if (Factor.Strategy == DecisionFactorAnswerStrategy.Manual)
			return Answer.HasValue ? Answer.Value ? 1 : -1 : 0;
		else
			return Factor.Points(asset);
	}
}
