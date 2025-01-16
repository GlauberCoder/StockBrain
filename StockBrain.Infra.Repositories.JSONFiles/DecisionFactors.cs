using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class DecisionFactors : BaseJSONFIleRepository<DecisionFactor, DecisionFactor>, IDecisionFactors
{
	public DecisionFactors(Context context, DataJSONFilesConfig config)
		: base(context, config, "decisionFactors")
	{
	}

	protected override DecisionFactor FromDTO(DecisionFactor dto) => dto;

	protected override IEnumerable<DecisionFactor> FromDTO(IEnumerable<DecisionFactor> dtos) => dtos;

	protected override DecisionFactor FromEntity(DecisionFactor entity) => entity;

	protected override IEnumerable<DecisionFactor> FromEntity(IEnumerable<DecisionFactor> entities) => entities;
}
