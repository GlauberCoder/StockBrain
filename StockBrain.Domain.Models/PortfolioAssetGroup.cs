using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class PortfolioAssetGroup
{
	public PortfolioAssetGroup()
	{

	}
	public PortfolioAssetGroup(double total, double target, string name, double currentValue, double investedValue)
	{
		var targetValue = new PercentageValue(target, total, 2);
		Name = name;
		Current = new PercentageValue(currentValue, total);
		Target = targetValue;
		DeltaInvested = new DeltaValue(investedValue, currentValue);
		DeltaTarget = new DeltaValue(currentValue, targetValue.Value);
	}
	public string Name { get; init; }
	public PercentageValue Current { get; init; }
	public PercentageValue Target { get; init; }
	public DeltaValue DeltaTarget { get; init; }
	public DeltaValue DeltaInvested { get; init; }

}
