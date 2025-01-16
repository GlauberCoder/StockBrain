using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.Model;

public class InvestmentRecommendationTypeConfig
{
	public AssetType Type { get; }
	public bool Invest { get; set; }
	public int Amount { get; set; }
	public double Target { get; set; }
	public InvestmentRecommendationTypeConfig(AssetType type, double target) : this(type, target, true, 3)
	{
	}
	public InvestmentRecommendationTypeConfig(AssetType type, double target, bool invest, int amount)
	{
		Type = type;
		Target = target;
		Invest = invest;
		Amount = amount;
	}
}
