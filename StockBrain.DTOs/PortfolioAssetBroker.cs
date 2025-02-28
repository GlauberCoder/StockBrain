namespace StockBrain.Domain.Models;

public class PortfolioAssetBrokerDTO : BaseEntity
{

	public PortfolioAssetBrokerDTO()
	{

	}
	public PortfolioAssetBrokerDTO(PortfolioAssetBroker asset)
	{
		GUID = asset.Broker.GUID;
		Quantity = asset.Quantity;
		Ticker = asset.Ticker;
		BrokerGUID = asset.Broker.GUID;

	}

	public string BrokerGUID { get; init; }
	public string Ticker { get; init; }
	public int Quantity { get; init; }

	public PortfolioAssetBroker ToEntity(Broker broker) 
	{ 
		return new PortfolioAssetBroker
		{
			GUID = BrokerGUID,
			Quantity = Quantity,
			Ticker = Ticker,
			Broker = broker
		};

	}
}
