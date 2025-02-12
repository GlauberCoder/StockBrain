using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Utils;
using System.Reflection.Metadata;

namespace StockBrain.Domain.Models.AssetInfos;

public class REITStats
{
	public REITStats(PortfolioAsset asset, REITInfo info, REITEvaluationConfig config)
	{
		Asset = asset;
		Info = info;
		Config = config;
		DividendAVG = info.Dividends.Any() ? info.Dividends.Take(config.BazinYearAmount).Average(d => d.Value).ToPrecision(2) : 0;
		BazinPrice = (DividendAVG / config.BazinExpectedReturn).ToPrecision(2);
		SlowAvg = info.Prices.Take(config.SlowAvgSize).Average(p => p.Value).ToPrecision(2);
		FastAvg = info.Prices.Take(config.FastAvgSize).Average(p => p.Value).ToPrecision(2);
		DownTrend = FastAvg < SlowAvg;
		HasEnoughYearsOfIPO = asset.Asset.IPO.Span.Years() >= config.IPOTimeThreshold;
		DYAvgRecent = info.DividendYields.Take(config.DividendYieldRecentAmount).Average(d => d.Value);
		DYAvgConsolidated = info.DividendYields.Take(config.DividendYieldConsolidatedAmount).Average(d => d.Value);
		BazinCeilingPriceAboveCurrent = BazinPrice >= asset.Asset.MarketPrice;
		CurrentPriceBelowPortfolioAverage = asset.Asset.MarketPrice <= asset.AveragePrice;
		HasEnoughYearsOfIPO = asset.Asset.IPO.Span.Years() >= config.IPOTimeThreshold;
		DownTrend = FastAvg <= SlowAvg;
		PVPBellowThreshold = info.PVP <= config.PVPThreshold;
		ManagementFeeBellowThreshold = info.ManagementFee <= config.ManagementFeeThreshold;
		VacancyBellowThreshold = info.VacancyRate <= config.VacancyRateThreshold;
		AssetValueAboveThreshold = info.AssetValue >= config.AssetValueThreshold;
		RegionsAboveThreshold = info.RegionCount >= config.RegionsThreshold;
		PropertyAmountAboveThreshold = info.PropertyCount >= config.PropertyThreshold;
		DailyLiquidityAboveThreshold = info.DailyLiquidity >= config.DailyLiquidityThreshold;
		DYAboveThresholdRecent = DYAvgRecent >= config.DividendYieldRecentThreshold;
		DYAboveThresholdConsolidated = DYAvgConsolidated >= config.DividendYieldRecentThreshold;
		RealROIAboveThresholdNear = info.RealROINear >= config.RealROIThresholdNear;
		RealROIAboveThresholdMiddle = info.RealROIMiddle >= config.RealROIThresholdMiddle;
		RealROIAboveThresholdLong = info.RealROILong >= config.RealROIThresholdLong;
		NominalROIAboveThresholdNear = info.NominalROINear >= config.NominalROIThresholdNear;
		NominalROIAboveThresholdMiddle = info.NominalROIMiddle >= config.NominalROIThresholdMiddle;
		NominalROIAboveThresholdLong = info.NominalROILong >= config.NominalROIThresholdLong;

	}
	public PortfolioAsset Asset { get; }
	public REITInfo Info { get; }
	public REITEvaluationConfig Config { get; }
	public double BazinPrice { get; }
	public double DividendAVG { get; }
	public double SlowAvg { get; }
	public double FastAvg { get; }
	public double DYAvgRecent { get; }
	public double DYAvgConsolidated { get; }
	public bool BazinCeilingPriceAboveCurrent { get; }
	public bool CurrentPriceBelowPortfolioAverage { get; }
	public bool HasEnoughYearsOfIPO { get; }
	public bool DownTrend { get; }
	public bool PVPBellowThreshold { get; set; }
	public bool ManagementFeeBellowThreshold { get; set; }
	public bool VacancyBellowThreshold { get; set; }
	public bool AssetValueAboveThreshold { get; set; }
	public bool RegionsAboveThreshold { get; set; }
	public bool PropertyAmountAboveThreshold { get; set; }
	public bool DailyLiquidityAboveThreshold { get; set; }
	public bool DYAboveThresholdRecent { get; set; }
	public bool DYAboveThresholdConsolidated { get; set; }
	public bool RealROIAboveThresholdNear { get; set; }
	public bool RealROIAboveThresholdMiddle { get; set; }
	public bool RealROIAboveThresholdLong { get; set; }
	public bool NominalROIAboveThresholdNear { get; set; }
	public bool NominalROIAboveThresholdMiddle { get; set; }
	public bool NominalROIAboveThresholdLong { get; set; }
}
