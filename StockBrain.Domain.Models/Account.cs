namespace StockBrain.Domain.Models
{
	public class Account : BaseEntity
	{
		public required string Name { get; init; }
		public required string MainPortfolio { get; init; }
	}
}
