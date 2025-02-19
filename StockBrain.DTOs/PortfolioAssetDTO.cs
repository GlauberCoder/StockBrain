namespace StockBrain.Domain.Models;

public class PortfolioAssetDTO : BaseEntity
{
	public PortfolioAssetDTO()
	{

	}
	public PortfolioAssetDTO(PortfolioAsset asset)
	{
		PortifolioID = asset.PortfolioID;
		ID = asset.ID;
		GUID = asset.GUID;
		Ticker = asset.Asset.Ticker;
		Quantity = asset.Quantity;
		Value = asset.InvestedValue;
		Risk = asset.Risk;
		FirstAquisition = asset.FirstAquisition;
		LastAquisition = asset.LastAquisition;
	}
	public long PortifolioID { get; set; }
	public string Ticker { get; set; }
	public int Quantity { get; set; }
	public double Value { get; set; }
	public bool Risk { get; set; }
	public DateOnly FirstAquisition { get; set; }
	public DateOnly LastAquisition { get; set; }

	public PortfolioAsset ToEntity(Asset asset, IEnumerable<PortfolioAssetMovement> movements, IEnumerable<PortfolioAssetBroker> brokers, Context context)
	{

		return new PortfolioAsset
		{
			ID = ID,
			PortfolioID = PortifolioID,
			GUID = GUID,
			InvestedValue = Value,
			Risk = Risk,
			Quantity = Quantity,
			FirstAquisition = FirstAquisition,
			LastAquisition = LastAquisition,
			Asset = asset,
			Movements = movements,
			Brokers = brokers
		};
	}
}
