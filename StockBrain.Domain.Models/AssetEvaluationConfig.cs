namespace StockBrain.Domain.Models;

public class AssetEvaluationConfig
{
	public required int SlowAvgSize { get; init; }
	public required int FastAvgSize { get; init; }
	public required double BazinStockExpectedReturn { get; init; }
	public required double StockGoodAge { get; init; }
	public required double StockGoodIPOTime { get; init; }
	public required double GrahamConstant { get; init; }
	public required int BazinYearAmount { get; init; }
	public required double GoodDailyLiquidity { get; init; }
	public required double StockGoodROE { get; init; }
}
