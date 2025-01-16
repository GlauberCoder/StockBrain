using StockBrain.Domain.Models;

namespace StockBrain.DTOs;

public class AssetDecisionFactorDTO : BaseEntity
{
	public AssetDecisionFactorDTO()
	{

	}
	public AssetDecisionFactorDTO(AssetDecisionFactor factor)
	{
		ID = factor.ID;
		GUID = factor.GUID;
		FactorID = factor.Factor.ID;
		Answer = factor.Answer;
		AssetID = factor.AssetID;
	}
	public long AssetID { get; init; }
	public long FactorID { get; init; }
	public bool? Answer { get; init; }
	public AssetDecisionFactor ToAssetDecisionFactor(DecisionFactor factor, Context context)
	{
		return new AssetDecisionFactor
		{
			ID = ID,
			GUID = GUID,
			Factor = factor,
			Answer = Answer,
			AssetID = AssetID
			
		};
	}
}
