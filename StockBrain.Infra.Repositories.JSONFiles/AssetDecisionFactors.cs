using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class AssetDecisionFactors : BaseJSONFIleRepository<AssetDecisionFactor, AssetDecisionFactorDTO>, IAssetDecisionFactors
{
	IDecisionFactors DecisionFactors { get; }

	public AssetDecisionFactors(Context context, DataJSONFilesConfig config, IDecisionFactors decisionFactors)
		: base(context, config, "assetDecisionFactors")
	{
		DecisionFactors = decisionFactors;
	}


	protected override AssetDecisionFactor FromDTO(AssetDecisionFactorDTO dto)
	{
		var factor = DecisionFactors.All().First(s => s.ID == dto.FactorID);
		return dto.ToAssetDecisionFactor(factor, Context);
	}

	protected override AssetDecisionFactorDTO FromEntity(AssetDecisionFactor entity) => new AssetDecisionFactorDTO(entity);

	protected override IEnumerable<AssetDecisionFactor> FromDTO(IEnumerable<AssetDecisionFactorDTO> dtos)
	{
		var factors = DecisionFactors.All().ToDictionary(s => s.ID, s => s);
		var assets = new List<AssetDecisionFactor>();
		foreach (var dto in dtos)
		{
			assets.Add(dto.ToAssetDecisionFactor(factors[dto.FactorID],  Context));
		}
		return assets;
	}

	protected override IEnumerable<AssetDecisionFactorDTO> FromEntity(IEnumerable<AssetDecisionFactor> entities) => entities.Select(FromEntity);
}
