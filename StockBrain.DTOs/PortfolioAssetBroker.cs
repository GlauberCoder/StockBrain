namespace StockBrain.Domain.Models;

public class PortfolioAssetBrokerDTO : BaseEntity
{

	public PortfolioAssetBrokerDTO()
	{

	}
	public PortfolioAssetBrokerDTO(PortfolioAssetBroker asset)
	{
		ID = asset.ID;
		GUID = asset.GUID;
		Quantity = asset.Quantity;
		PortfolioID = asset.PortfolioID;
		Ticker = asset.Ticker;
		BrokerID = asset.Broker.ID;

	}

	public long PortfolioID { get; init; }
	public long BrokerID { get; init; }
	public string Ticker { get; init; }
	public int Quantity { get; init; }

	public PortfolioAssetBroker ToEntity(Broker broker) 
	{ 
		return new PortfolioAssetBroker
		{
			ID = ID,
			GUID = GUID,
			Quantity = Quantity,
			Ticker = Ticker,
			PortfolioID = PortfolioID,
			Broker = broker
		};

	}
}
