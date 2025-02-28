using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class Asset : BaseEntity
{
	public override string GUID { get { return Ticker; } set {} }
	public required string Name { get; init; }
	public required string Ticker { get; init; }
	public required string Description { get; init; }
	public required string PositiveNotes { get; init; }
	public required string NegativeNotes { get; init; }
	public required DateOnly LastPriceUpdate { get; set; }
	public required DateOnlySpan LastReview { get; set; }
	public required bool ReviewExpired { get; init; }
	public required bool Risk { get; init; }
	public required DateOnlySpan IPO { get; init; }
	public required DateOnlySpan Foundation { get; init; }
	public required AssetType Type { get; init; }
	public required Sector Sector { get; init; }
	public required Segment Segment { get; init; }
	public double? MarketPrice { get; set; }

	public override bool Equals(object o)
	{
		var other = o as Asset;

		return other?.GUID == GUID;
	}

	public override string ToString() => Ticker;

	public override int GetHashCode() => base.GetHashCode();
}
