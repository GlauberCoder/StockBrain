using StockBrain.Domain.Models.Enumerations;

namespace StockBrain.Domain.Models;

public class DecisionFactorAnswer
{
	public required DecisionFactor Factor { get; init; }
	public required bool Answer { get; init; }
}
