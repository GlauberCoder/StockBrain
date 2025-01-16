namespace StockBrain.Domain.Models;

public class AssetMovement : BaseEntity
{
	public long AccountID { get; set; }
	public Asset Asset { get; set; }
	public Broker Broker { get; set; }
	public int Quantity { get; set; }
	public double Investment { get; set; }
	public DateOnly Date { get; set; }
}
