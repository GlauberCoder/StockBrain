namespace StockBrain.Domain.Models;

public class PortfolioAssetMovement : BaseEntity
{
	public PortfolioAssetMovement()
	{

	}
	public PortfolioAssetMovement(AssetMovement movement, PortfolioAsset asset, Context context)
	{
		PortfolioID = asset.PortfolioID;
		Ticker = movement.Asset.Ticker;
		StartQuantity = asset.Quantity;
		StartInvestment = asset.InvestedValue;
		Quantity = movement.Quantity;
		Investment = movement.Investment;
		Date = context.Today;
		Broker = movement.Broker;

	}
	public long PortfolioID { get; init; }
	public Broker Broker { get; init; }
	public string Ticker { get; init; }
	public int StartQuantity { get; init; }
	public double StartInvestment { get; init; }
	public int Quantity { get; init; }
	public double Investment { get; init; }
	public int EndQuantity => StartQuantity + Quantity;
	public double EndInvestment => StartInvestment + Investment;
	public DateOnly Date { get; init; }
}
