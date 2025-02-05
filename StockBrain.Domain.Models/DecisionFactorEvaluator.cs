using StockBrain.Domain.Models.Enumerations;

namespace StockBrain.Domain.Models;

public class DecisionFactorEvaluator<TStats>
{
	public required DecisionFactor Factor { get; init; }
	public required Func<TStats, bool> Evaluator { get; init; }
	public required Func<TStats, IEnumerable<string>> NameParts { get; init; }
	public required Func<TStats, IEnumerable<string>> DescriptionPartsParts { get; init; }
	public DecisionFactorAnswer Answer(TStats stats) => new DecisionFactorAnswer { Factor = Factor.CompleteName(NameParts(stats), DescriptionPartsParts(stats)), Answer = Evaluator(stats) };
}
