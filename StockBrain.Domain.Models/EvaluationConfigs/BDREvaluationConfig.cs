namespace StockBrain.Domain.Models.EvaluationConfigs;

public class BDREvaluationConfig
{
	public required int SlowAvgSize { get; init; }
	public required int FastAvgSize { get; init; }
	public required double BazinExpectedReturn { get; init; }
	public required double AgeThreshold { get; init; }
	public required double PLThreshold { get; init; }
	public required double PVPThreshold { get; init; }
	public required double IPOTimeThreshold { get; init; }
	public required double DividendYieldThreshold { get; init; }
	public required int DividendYieldTimeInYears { get; init; }
	public required int RevenueGrowthTimeInYears { get; init; }
	public required int ProfitableTimeInQuarters { get; init; }
	public required int ProfitGrowthTimeInYears { get; init; }
	public required double GrahamConstant { get; init; }
	public required int BazinYearAmount { get; init; }
	public required double DailyLiquidityThreshold { get; init; }
	public required double ROEThreshold { get; init; }
	public required int NearROIInYears { get; init; }
	public required int MiddleROIInYears { get; init; }
	public required int LongROIInYears { get; init; }
	public required double RealROIThresholdNear { get; init; }
	public required double RealROIThresholdMiddle { get; init; }
	public required double RealROIThresholdLong { get; init; }
	public required double NominalROIThresholdNear { get; init; }
	public required double NominalROIThresholdMiddle { get; init; }
	public required double NominalROIThresholdLong { get; init; }
}
