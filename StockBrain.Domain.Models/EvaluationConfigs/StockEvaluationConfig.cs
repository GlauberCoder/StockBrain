namespace StockBrain.Domain.Models.EvaluationConfigs;

public class StockEvaluationConfig
{
	public required int SlowAvgSize { get; init; }
	public required int FastAvgSize { get; init; }
	public required double BazinExpectedReturn { get; init; }
	public required double AgeThreshold { get; init; }
	public required double IPOTimeThreshold { get; init; }
	public required double DividendYieldThreshold { get; init; }
	public required int DividendYieldTimeInYears { get; init; }
	public required int RevenueGrowthTimeInYears { get; init; }
	public required int ProfitGrowthTimeInYears { get; init; }
	public required double GrahamConstant { get; init; }
	public required int BazinYearAmount { get; init; }
	public required double DailyLiquidityThreshold { get; init; }
	public required double ROEThreshold { get; init; }
}
