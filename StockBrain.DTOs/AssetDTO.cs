using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.DTOs;

public class AssetDTO : BaseEntity
{
	public AssetDTO()
	{

	}
	public AssetDTO(Asset asset)
	{
		GUID = asset.GUID;
		Ticker = asset.Ticker;
		Type = asset.Type;
		Name = asset.Name;
		Description = asset.Description;
		Risk = asset.Risk;
		PositiveNotes = asset.PositiveNotes;
		NegativeNotes = asset.NegativeNotes;
		LastReview = asset.LastReview.Date;
		IPO = asset.IPO.Date;
		Foundation = asset.Foundation.Date;
		SectorGUID = asset.Sector.GUID;
		SegmentGUID = asset.Segment.GUID;
		MarketPrice = asset.MarketPrice;
		LastPriceUpdate = asset.LastPriceUpdate;
	}
	public override string GUID { get => Ticker; set { } }
	public string Ticker { get; set; }
	public string Description { get; set; }
	public bool Risk { get; set; }
	public string PositiveNotes { get; set; }
	public string NegativeNotes { get; set; }
	public DateOnly LastReview { get; set; }
	public DateOnly LastPriceUpdate { get; set; }
	public DateOnly IPO { get; set; }
	public DateOnly Foundation { get; set; }
	public AssetType Type { get; set; }
	public string Name { get; set; }
	public string SectorGUID { get; set; }
	public string SegmentGUID { get; set; }
	public double? MarketPrice { get; set; }

	public Asset ToAsset(Sector sector, Segment segment, Context context) 
	{
		var maxMonthsToExpire = 3;
		return new Asset
		{
			GUID = GUID,
			Ticker = Ticker,
			Type = Type,
			Name = Name,
			Description = Description,
			Risk = Risk,
			PositiveNotes = PositiveNotes,
			NegativeNotes = NegativeNotes,
			LastReview = new DateOnlySpan(LastReview, context.Today),
			ReviewExpired = LastReview.AddMonths(maxMonthsToExpire) < context.Today,
			IPO = new DateOnlySpan(IPO, context.Today),
			Foundation = new DateOnlySpan(Foundation, context.Today),
			MarketPrice = MarketPrice,
			Sector = sector,
			Segment = segment,
			LastPriceUpdate = LastPriceUpdate
		};
	}
}
