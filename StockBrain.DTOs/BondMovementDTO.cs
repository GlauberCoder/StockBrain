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
		GUID = movement.GUID;
		BrokerGUID = movement.Broker.GUID;
		IssuerGUID = movement.Issuer.GUID;
		Index = movement.Index;
		Tax = movement.Tax;
		Value = movement.Value;
		Expiration = movement.Expiration;
		Date = movement.Date;

	}
	public string BrokerGUID { get; set; }
	public string IssuerGUID { get; set; }
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
			GUID = GUID,
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
