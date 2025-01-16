namespace StockBrain.Domain.Models;

public class DeltaValue
{
	public double Difference { get; }
	public double Percentage { get; }
	public double Initial { get; }
	public double Final { get; }

	public DeltaValue(double initial, double final)
	{
		Initial = initial;
		Final = final;
		Difference = final - initial;
		Percentage = Difference / initial;
	}
}
