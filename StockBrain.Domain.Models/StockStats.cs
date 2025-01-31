using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class StockStats : StockInfo
{
	public StockStats(Asset asset) : base(asset)
	{

	}
	public double BazinPrice { get; }
	public double GrahamPrice { get; }
	public double DividendAVG { get; }
	public double SlowAvg { get; }
	public double FastAvg { get; }
	public bool HasAcceptableROE { get; }
	public bool LowDebtToEquity { get; }
	public bool PositiveRevenueCAGR { get; }
	public bool PositiveProfitCAGR { get; }
	public bool HasLiquidity { get; }

}
