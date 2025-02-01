using StockBrain.Domain.Models.Enumerations;

namespace StockBrain.Domain.Models;

public class DecisionFactorEvaluator<TStats>
{
	public required DecisionFactor Factor { get; init; }
	public required Func<TStats, bool> Evaluator { get; init; }
	public DecisionFactorAnswer Answer(TStats stats) => new DecisionFactorAnswer { Factor = Factor, Answer = Evaluator(stats) };
}
