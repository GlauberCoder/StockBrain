using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class InvestmentGroupType : InvestmentGroup
{
	public required AssetType Type { get; init; }
	public required int? MaxRecommendations { get; init; }
	public IEnumerable<InvestmentGroupAsset> Assets { get; set; }
	public override int Level => 2;

}
