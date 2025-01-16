using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class PercentageValue
{
	public double Value { get; }
	public double Proportion { get; }

	public PercentageValue(double value, double total)
	{
		Value = value;
		Proportion = value/total;
	}
	public PercentageValue(double percentage, double total, int? precision)
	{
		Value = total * percentage;
		if(precision.HasValue)
			Value = Value.ToPrecision(precision.Value);
		Proportion = percentage;
	}
}
