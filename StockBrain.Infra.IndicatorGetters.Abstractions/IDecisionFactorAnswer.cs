using StockBrain.Domain.Models;

namespace StockBrain.Infra.IndicatorGetters.Abstractions;

public interface IDecisionFactorAnswer
{
	Task<IEnumerable<Asset>> All();
}
