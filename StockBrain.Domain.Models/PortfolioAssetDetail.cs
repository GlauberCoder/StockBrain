namespace StockBrain.Domain.Models;

public class PortfolioAssetDetail 
{
	public required PortfolioAsset Asset { get; init; }
	public required PercentageValue InvestedOnTotal { get; init; }
	public required PercentageValue InvestedType { get; init; }
	public required PercentageValue Target { get; init; }
	public required DeltaValue DeltaTarget { get; init; }
}
