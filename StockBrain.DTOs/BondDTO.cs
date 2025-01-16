using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class BondDTO : BaseEntity
{
	public BondDTO()
	{

	}
	public BondDTO(Bond bond)
	{
		ID = bond.ID;
		GUID = bond.GUID;
		Value = bond.Value;
		BrokerID = bond.Broker.ID;
		IssuerID = bond.Issuer.ID;
		Type = bond.Type;
		Index = bond.Index;
		Tax = bond.Tax;
		Date = bond.Date;
		Expiration = bond.Expiration;
		Redeem = bond.Redeem;
		PortifolioID = bond.PortfolioID;
	}
	public long BrokerID { get; set; }
	public long IssuerID { get; set; }
	public BondType Type { get; set; }
	public BondIndex Index { get; set; }
	public double Tax { get; set; }
	public double Value { get; set; }
	public DateOnly? Redeem { get; set; }
	public DateOnly Date { get; set; }
	public DateOnly Expiration { get; set; }
	public long PortifolioID { get; set; }

	public Bond ToBond(Broker broker, BondIssuer issuer, Context context)
	{
		var bond = new Bond
		{
			ID = ID,
			GUID = GUID,
			PortfolioID = PortifolioID,
			Value = Value,
			Broker = broker,
			Issuer = issuer,
			Type = Type,
			Index = Index,
			Tax = Tax,
			Date = Date,
			Expiration = Expiration,
			Redeem = Redeem,
			Expired = context.Today > Expiration,
			TimeToExpire = Expiration.TimeSpanBetween(context.Now),
			Age = context.Now.TimeSpanBetween(Date)
		};
		return bond;
	}
}
