using StockBrain.Domain.Models;

namespace StockBrain.DTOs;

public class AssetMovementDTO : BaseEntity
{
	public AssetMovementDTO()
	{

	}
	public AssetMovementDTO(AssetMovement movement)
	{
		GUID = movement.GUID;
		AssetGUID = movement.Asset.GUID;
		Quantity = movement.Quantity;
		Investment = movement.Investment;
		Date = movement.Date;
		BrokerGUID = movement.Broker?.GUID;

	}
	public string AssetGUID { get; set; }
	public string? BrokerGUID { get; set; }
	public int Quantity { get; set; }
	public bool Confirmed { get; set; }
	public double Investment { get; set; }
	public DateOnly Date { get; set; }

	public AssetMovement ToEntity(Asset asset, Broker broker, Context context)
	{
		return new AssetMovement
		{
			GUID = GUID,
			Asset = asset,
			Quantity = Quantity,
			Investment = Investment,
			Date = Date,
			Broker = broker
		};
	}

}
