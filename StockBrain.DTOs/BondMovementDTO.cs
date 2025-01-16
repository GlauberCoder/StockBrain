using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.DTOs;
public class BondMovementDTO : BaseEntity
{
	public BondMovementDTO()
	{

	}
	public BondMovementDTO(BondMovement movement, Context context)
	{
		ID = movement.ID;
		GUID = movement.GUID;
		AccountID = movement.AccountID;
		BrokerID = movement.Broker.ID;
		IssuerID = movement.Issuer.ID;
		Index = movement.Index;
		Tax = movement.Tax;
		Value = movement.Value;
		Expiration = movement.Expiration;
		Date = movement.Date;

	}
	public long AccountID { get; set; }
	public long BrokerID { get; set; }
	public long IssuerID { get; set; }
	public BondType Type { get; set; } = BondType.CDB;
	public BondIndex Index { get; set; } = BondIndex.IPCA;
	public double Tax { get; set; }
	public double Value { get; set; }
	public DateOnly Expiration { get; set; }
	public DateOnly Date { get; set; }
	public BondMovement ToEntity(BondIssuer issuer, Broker broker, Context context)
	{
		return new BondMovement
		{
			ID = ID,
			GUID = GUID,
			AccountID = context.Account.ID,
			Broker = broker,
			Issuer = issuer,
			Type = Type,
			Index = Index,
			Tax = Tax,
			Value = Value,
			Expiration = Expiration,
			Date = Date
		};
	}
}
