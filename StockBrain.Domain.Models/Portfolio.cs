using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class Portfolio : BaseEntity
{
	public required string Name { get; init; }
	public required double Total { get; init; }
	public required IEnumerable<PortfolioAssetDetail> Assets { get; set; }
	public required IEnumerable<Bond> Bonds { get; set; }
	public required IDictionary<AssetType, PortfolioAssetGroup> Types { get; init; }
	public required IDictionary<AssetCategory, PortfolioAssetGroup> Categories { get; init; }
	public PortfolioAssetGroup Fix => Categories[AssetCategory.Fix];
	public PortfolioAssetGroup Variable => Categories[AssetCategory.Var];


}
