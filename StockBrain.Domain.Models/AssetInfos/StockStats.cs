using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Utils;

namespace StockBrain.Domain.Models.AssetInfos;

public class StockStats
{
	public StockStats(PortfolioAsset asset, StockInfo info, StockEvaluationConfig config)
	{
		Info = info;
		Config = config;
		DividendAVG = info.Dividends?.Take(config.BazinYearAmount).Average(d => d.Value).ToPrecision(4) ?? 0;
		BazinPrice = (DividendAVG / config.BazinExpectedReturn).ToPrecision(2);
		GrahamPrice = Math.Sqrt(config.GrahamConstant * info.LPA * info.VPA).ToPrecision(2);
		SlowAvg = info.Prices.Take(config.SlowAvgSize).Average(p => p.Value).ToPrecision(2);
		FastAvg = info.Prices.Take(config.FastAvgSize).Average(p => p.Value).ToPrecision(2);
		DownTrend = FastAvg < SlowAvg;
		HasLiquidity = info.DailyLiquidity > config.DailyLiquidityThreshold;
		LowDebtToEquity = info.Debt / info.Equity < 1;
		HasAcceptableROE = info.ROE >= config.ROEThreshold;
		PositiveRevenueCAGR = info.RevenueCAGR > 0;
		PositiveProfitCAGR = info.ProfitCAGR > 0;
		HasEnoughYearsInMarket = asset.Asset.Foundation.Span.Years() >= config.AgeThreshold;
		HasEnoughYearsOfIPO = asset.Asset.IPO.Span.Years() >= config.IPOTimeThreshold;
		CurrentPriceBelowPortfolioAverage = asset.Asset.MarketPrice < asset.AveragePrice;
		BazinCeilingPriceAboveCurrent = BazinPrice > asset.Asset.MarketPrice;
		GrahamFairPriceAboveCurrent = GrahamPrice > asset.Asset.MarketPrice;
		RealROIAboveThresholdNear = info.RealROINear >= config.RealROIThresholdNear;
		RealROIAboveThresholdMiddle = info.RealROIMiddle >= config.RealROIThresholdMiddle;
		RealROIAboveThresholdLong = info.RealROILong >= config.RealROIThresholdLong;
		NominalROIAboveThresholdNear = info.NominalROINear >= config.NominalROIThresholdNear;
		NominalROIAboveThresholdMiddle = info.NominalROIMiddle >= config.NominalROIThresholdMiddle;
		NominalROIAboveThresholdLong = info.NominalROILong >= config.NominalROIThresholdLong;
		PLIsNotTooHigh = info.PL <= config.PLThreshold;
		PVPIsNotTooHigh = info.PVP <= config.PVPThreshold;

	}
	public StockInfo Info { get; }
	public StockEvaluationConfig Config { get; }
	public double BazinPrice { get; }
	public double GrahamPrice { get; }
	public double DividendAVG { get; }
	public double SlowAvg { get; }
	public double FastAvg { get; }
	public bool BazinCeilingPriceAboveCurrent { get; }
	public bool GrahamFairPriceAboveCurrent { get; }
	public bool CurrentPriceBelowPortfolioAverage { get; }
	public bool HasEnoughYearsInMarket { get; }
	public bool HasEnoughYearsOfIPO { get; }
	public bool HasAcceptableROE { get; }
	public bool LowDebtToEquity { get; }
	public bool PositiveRevenueCAGR { get; }
	public bool PositiveProfitCAGR { get; }
	public bool HasLiquidity { get; }
	public bool PLIsNotTooHigh { get; }
	public bool PVPIsNotTooHigh { get; }
	public bool DownTrend { get; }
	public bool RealROIAboveThresholdNear { get; set; }
	public bool RealROIAboveThresholdMiddle { get; set; }
	public bool RealROIAboveThresholdLong { get; set; }
	public bool NominalROIAboveThresholdNear { get; set; }
	public bool NominalROIAboveThresholdMiddle { get; set; }
	public bool NominalROIAboveThresholdLong { get; set; }

}
