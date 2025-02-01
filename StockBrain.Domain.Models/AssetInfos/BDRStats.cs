using StockBrain.Utils;

namespace StockBrain.Domain.Models.AssetInfos;

public class BDRStats
{
	public BDRStats(PortfolioAsset asset, BDRInfo info, AssetEvaluationConfig config)
	{
		Asset = asset;
		Info = info;
		DividendAVG = info.Dividends.Any() ? info.Dividends.Take(config.BazinYearAmount).Average(d => d.Value).ToPrecision(2) : 0;
		BazinPrice = (DividendAVG / config.BazinStockExpectedReturn).ToPrecision(2);
		GrahamPrice = Math.Sqrt(config.GrahamConstant * info.LPA * info.VPA).ToPrecision(2);
		SlowAvg = info.Prices.Take(config.SlowAvgSize).Average(p => p.Value).ToPrecision(2);
		FastAvg = info.Prices.Take(config.FastAvgSize).Average(p => p.Value).ToPrecision(2);
		DownTrend = FastAvg < SlowAvg;
		HasAcceptableROE = info.ROE >= config.StockGoodROE;
		HasEnoughYearsInMarket = asset.Asset.Foundation.Span.Years() >= config.StockGoodAge;
		HasEnoughYearsOfIPO = asset.Asset.IPO.Span.Years() >= config.StockGoodIPOTime;
		CurrentPriceBelowPortfolioAverage = asset.Asset.MarketPrice < asset.AveragePrice;
		BazinCeilingPriceAboveCurrent = BazinPrice > asset.Asset.MarketPrice;
		GrahamFairPriceAboveCurrent = GrahamPrice > asset.Asset.MarketPrice;

	}
	public PortfolioAsset Asset { get; }
	public BDRInfo Info { get; }
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
	public bool DownTrend { get; }

}
