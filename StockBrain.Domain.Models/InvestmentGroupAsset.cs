using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class InvestmentGroupAsset :  InvestmentGroup
{
	public PortfolioAssetDetail Asset { get; set; }
	public double AfterAvgPrice { get; private set; }
	public override int Level => 3;
	public override InvestmentGroup SetInvestment(double investmentPercentage, double totalInvestment, double newTotal)
	{
		base.SetInvestment(investmentPercentage, totalInvestment, newTotal);
		if (Asset.Asset.Asset.MarketPrice.HasValue)
		{
			Quantity = (int)(Investment.Value / Asset.Asset.Asset.MarketPrice.Value);
			AfterAvgPrice = ((Asset.Asset.CurrentValue + Investment.Value) / (double)(Asset.Asset.Quantity + Quantity)).ToPrecision(2);
		}
		return this;
	}
}
