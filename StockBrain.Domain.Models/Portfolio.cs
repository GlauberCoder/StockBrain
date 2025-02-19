using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class Portfolio : BaseEntity
{
	public required long AccountID { get; init; }
	public required string Name { get; init; }
	public required double Total { get; init; }
	public required bool Main { get; init; }
	public required IEnumerable<PortfolioAssetDetail> Assets { get; init; }
	public required IEnumerable<Bond> Bonds { get; init; }
	public required IDictionary<AssetType, PortfolioAssetGroup> Types { get; init; }
	public required IDictionary<AssetCategory, PortfolioAssetGroup> Categories { get; init; }
	public PortfolioAssetGroup Fix => Categories[AssetCategory.Fix];
	public PortfolioAssetGroup Variable => Categories[AssetCategory.Var];


}
