namespace StockBrain.Domain.Models;

public class PortfolioAssetBroker : BaseEntity
{
	public required string Ticker { get; init; }
	public required Broker Broker { get; init; }
	public required int Quantity { get; set; }
}
