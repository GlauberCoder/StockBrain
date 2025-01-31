namespace StockBrain.Domain.Models;

public class DecisionFactor<TStats>
{
	public required string Key { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }
	public required Func<TStats, bool> Evaluator { get; init; }
}
