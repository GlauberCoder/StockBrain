using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Assets(Context context, DataJSONFilesConfig config, ISectors sectors, ISegments segments, IAssetDecisionFactors AssetDecisionFactors, IDecisionFactors decisionFactors) 
	: BaseJSONFIleRepository<Asset, AssetDTO>(context, config, "assets"), IAssets
{
	ISectors Sectors { get; } = sectors;
	ISegments Segments { get; } = segments;
	IAssetDecisionFactors AssetDecisionFactors { get; } = AssetDecisionFactors;
	IDecisionFactors DecisionFactors { get; } = decisionFactors;

	protected override Asset FromDTO(AssetDTO dto)
	{
		var sector = Sectors.All().First(s => s.ID == dto.SectorID);
		var segment = Segments.All().First(s => s.ID == dto.SegmentID);
		var answers = GetAnswers(dto, DecisionFactors.All(), AssetDecisionFactors.All());

		return dto.ToAsset(sector, segment, answers, Context);
	}

	private List<AssetDecisionFactor> GetAnswers(AssetDTO dto, IEnumerable<DecisionFactor> decisionFactors, IEnumerable<AssetDecisionFactor> assetDecitionFactors)
	{
		var answers = new List<AssetDecisionFactor>();
		var factorsAnswers = assetDecitionFactors.Where(a => a.AssetID == dto.ID).ToDictionary(f => f.Factor.ID, f => f);
		foreach (var decisionFactor in decisionFactors)
		{
			if (decisionFactor.Type == dto.Type)
			{
				if (!factorsAnswers.TryGetValue(decisionFactor.ID, out var answer))
					answer = new AssetDecisionFactor { ID = 0, GUID = Guid.NewGuid().ToString(), Answer = null, AssetID = dto.ID, Factor = decisionFactor };
				answers.Add(answer);
			}
		}

		return answers;
	}

	protected override AssetDTO FromEntity(Asset entity) => new AssetDTO(entity);

	protected override IEnumerable<Asset> FromDTO(IEnumerable<AssetDTO> dtos)
	{
		var sectors = Sectors.All().ToDictionary(s => s.ID, s => s);
		var segments = Segments.All().ToDictionary(s => s.ID, s => s);
		var decisionFactors = DecisionFactors.All();
		var assetDecitionFactors = AssetDecisionFactors.All()
;		var assets = new List<Asset>();
		foreach (var dto in dtos)
		{
			var answers = GetAnswers(dto, decisionFactors, assetDecitionFactors);
			assets.Add(dto.ToAsset(sectors[dto.SectorID], segments[dto.SegmentID], answers, Context));
		}
		return assets;
	}

	protected override IEnumerable<AssetDTO> FromEntity(IEnumerable<Asset> entities) => entities.Select(FromEntity);

	public Asset SaveEvaluation(Asset asset)
	{
		asset.LastReview = new DateOnlySpan(Context.Today, Context.Today);
		Save(asset);
		AssetDecisionFactors.Save(asset.Factors);
		return asset;
	}

	public Asset ByTicker(string ticker) => FromDTO(AllDTO().First(a => a.Ticker == ticker));
}
