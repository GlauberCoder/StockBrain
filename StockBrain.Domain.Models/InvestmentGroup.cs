using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public abstract class  InvestmentGroup
{
	public string Name { get; set; }
	public abstract int Level { get; }
	public int? Quantity { get; set; }
	public PercentageValue Current { get; set; }
	public PercentageValue Target { get; set; }
	public DeltaValue DeltaTarget { get; set; }
	public PercentageValue After { get; set; }
	public PercentageValue Investment { get; set; }

	public virtual InvestmentGroup SetInvestment(double investmentPercentage, double totalInvestment, double newTotal) 
	{
		Investment = new PercentageValue(investmentPercentage, totalInvestment, 2);
		After = new PercentageValue(Current.Value + Investment.Value, newTotal);
		return this;
	}

}
