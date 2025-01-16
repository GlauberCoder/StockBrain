using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class BondMovement : BaseEntity
{
	public long AccountID { get; set; }
	public DateOnly Date { get; set; }
	public Broker Broker { get; set; }
	public BondIssuer Issuer { get; set; }
	public BondType Type { get; set; } = BondType.CDB;
	public BondIndex Index { get; set; } = BondIndex.IPCA;
	public double Tax { get; set; }
	public double Value { get; set; }
	public DateOnly Expiration { get; set; }

	public Bond ToBond(long portfolioID)
	{
		return new Bond
		{
			PortfolioID = portfolioID,
			Broker = Broker,
			Issuer = Issuer,
			Type = Type,
			Index = Index,
			Tax = Tax,
			Value = Value,
			Date = Date,
			Expiration = Expiration,
			Redeem = null,
			
		};
	}

}
