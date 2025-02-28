using StockBrain.DTOs;

namespace StockBrain.Domain.Models;

public class PortfolioAssetDTO : BaseEntity
{
	public PortfolioAssetDTO()
	{

	}
	public PortfolioAssetDTO(PortfolioAsset asset)
	{
		GUID = asset.Asset.Ticker;
		Ticker = asset.Asset.Ticker;
		Quantity = asset.Quantity;
		Value = asset.InvestedValue;
		Risk = asset.Risk;
		FirstAquisition = asset.FirstAquisition;
		LastAquisition = asset.LastAquisition;
		Movements = asset.Movements?.ToDictionary(m => m.GUID, m => new PortfolioAssetMovementDTO(m)) ?? new Dictionary<string, PortfolioAssetMovementDTO>();
		Brokers = asset.Brokers?.ToDictionary(m => m.GUID, m => new PortfolioAssetBrokerDTO(m)) ?? new Dictionary<string, PortfolioAssetBrokerDTO>();
	}
	public override string GUID { get { return Ticker; } set { } }
	public string Ticker { get; set; }
	public int Quantity { get; set; }
	public double Value { get; set; }
	public bool Risk { get; set; }
	public DateOnly FirstAquisition { get; set; }
	public DateOnly LastAquisition { get; set; }
	public IDictionary<string, PortfolioAssetMovementDTO> Movements { get; set; }
	public IDictionary<string, PortfolioAssetBrokerDTO> Brokers { get; set; }

	public PortfolioAsset ToEntity(Asset asset, IDictionary<string, Broker> brokers)
	{

		return new PortfolioAsset
		{
			GUID = asset.Ticker,
			InvestedValue = Value,
			Risk = Risk,
			Quantity = Quantity,
			FirstAquisition = FirstAquisition,
			LastAquisition = LastAquisition,
			Asset = asset,
			Movements = Movements?.Select(m => m.Value.ToEntity(brokers[m.Value.BrokerGUID])) ?? Enumerable.Empty<PortfolioAssetMovement>(),
			Brokers = Brokers?.Select(b => b.Value.ToEntity(brokers[b.Value.BrokerGUID])) ?? Enumerable.Empty<PortfolioAssetBroker>()
		};
	}
}
