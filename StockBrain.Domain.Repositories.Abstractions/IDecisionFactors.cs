using StockBrain.Domain.Models.Enums;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IDecisionFactors
{
	IDictionary<AssetType, IEnumerable<string>> All();
}
