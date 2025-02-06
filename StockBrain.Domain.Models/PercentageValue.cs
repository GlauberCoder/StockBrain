using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class PercentageValue
{
	public double Value { get; }
	public double Total { get; }
	public double Proportion { get; }

	public PercentageValue(double value, double total)
	{
		Value = value;
		Total = total;
		if(total > 0)
			Proportion = value/total;
	}
	public PercentageValue(double percentage, double total, int? precision)
	{
		Value = total * percentage;
		Total = total;
		if(precision.HasValue)
			Value = Value.ToPrecision(precision.Value);
		Proportion = percentage;
	}
}
