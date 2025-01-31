using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class StockStats 
{
	public StockStats(Asset asset, StockInfo info, AssetEvaluationConfig config)
	{
		Asset = asset;
		Info = info;
		DividendAVG = info.Dividends.Take(config.BazinYearAmount).Average(d => d.Value).ToPrecision(2);
		BazinPrice = (DividendAVG / config.BazinStockExpectedReturn).ToPrecision(2);
		GrahamPrice = Math.Sqrt(config.GrahamConstant * info.LPA * info.VPA).ToPrecision(2);
		SlowAvg = info.Prices.Take(config.SlowAvgSize).Average(p => p.Value).ToPrecision(2);
		FastAvg = info.Prices.Take(config.FastAvgSize).Average(p => p.Value).ToPrecision(2);
		DownTrend = FastAvg < SlowAvg;
		HasLiquidity = info.DailyLiquidity > config.GoodDailyLiquidity;
		LowDebtToEquity = (info.Debt / info.Equity) < 1;
		HasAcceptableROE = info.ROE >= config.StockGoodROE;
		PositiveRevenueCAGR = info.RevenueCAGR > 0;
		PositiveProfitCAGR = info.ProfitCAGR > 0;

	}
	public Asset Asset { get; }
	public StockInfo Info { get; }
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
	public bool DownTrend { get; }

}
