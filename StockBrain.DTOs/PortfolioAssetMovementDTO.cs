using StockBrain.Domain.Models;

namespace StockBrain.DTOs;

public class PortfolioAssetMovementDTO : BaseEntity
{
	public PortfolioAssetMovementDTO()
	{

	}
	public PortfolioAssetMovementDTO(PortfolioAssetMovement movement)
	{
		GUID = movement.GUID;
		BrokerGUID = movement.Broker.GUID;
		Quantity = movement.Quantity;
		Investment = movement.Investment;
		Date = movement.Date;
		Ticker = movement.Ticker;
		StartQuantity = movement.StartQuantity;
		StartInvestment = movement.StartInvestment;

	}
	public string BrokerGUID { get; set; }
	public string Ticker { get; set; }
	public int StartQuantity { get; set; }
	public double StartInvestment { get; set; }
	public int Quantity { get; set; }
	public double Investment { get; set; }
	public DateOnly Date { get; set; }

	public PortfolioAssetMovement ToEntity(Broker broker)
	{
		return new PortfolioAssetMovement
		{
			GUID = GUID,
			Broker = broker,
			Quantity = Quantity,
			Investment = Investment,
			Date = Date,
			Ticker = Ticker,
			StartQuantity = StartQuantity,
			StartInvestment = StartInvestment
		};
	}

}
