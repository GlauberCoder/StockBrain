namespace StockBrain.Domain.Models;

public class DecisionFactor
{
	public required string Key { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }
}
