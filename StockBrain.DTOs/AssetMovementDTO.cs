using StockBrain.Domain.Models;

namespace StockBrain.DTOs;

public class AssetMovementDTO : BaseEntity
{
	public AssetMovementDTO()
	{

	}
	public AssetMovementDTO(AssetMovement movement)
	{
		ID = movement.ID;
		GUID = movement.GUID;
		AccountID = movement.AccountID;
		AssetID = movement.Asset.ID;
		Quantity = movement.Quantity;
		Investment = movement.Investment;
		Date = movement.Date;
		BrokerID = movement.Broker?.ID;

	}
	public long AccountID { get; set; }
	public long AssetID { get; set; }
	public long? BrokerID { get; set; }
	public int Quantity { get; set; }
	public bool Confirmed { get; set; }
	public double Investment { get; set; }
	public DateOnly Date { get; set; }

	public AssetMovement ToEntity(Asset asset, Broker broker, Context context)
	{
		return new AssetMovement
		{
			ID = ID,
			GUID = GUID,
			AccountID = context.Account.ID,
			Asset = asset,
			Quantity = Quantity,
			Investment = Investment,
			Date = Date,
			Broker = broker
		};
	}

}
