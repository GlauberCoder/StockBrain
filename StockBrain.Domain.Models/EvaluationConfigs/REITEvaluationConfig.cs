﻿namespace StockBrain.Domain.Models.EvaluationConfigs;

public class REITEvaluationConfig
{
	public required int SlowAvgSize { get; init; }
	public required int FastAvgSize { get; init; }
	public required double BazinExpectedReturn { get; init; }
	public required double IPOTimeThreshold { get; init; }
	public required double PVPThreshold { get; init; }
	public required double ManagementFeeThreshold { get; init; }
	public required double VacancyRateThreshold { get; init; }
	public required double AssetValueThreshold { get; init; }
	public required int RegionsThreshold { get; init; }
	public required int PropertyThreshold { get; init; }
	public required double DividendYieldRecentThreshold { get; init; }
	public required double DividendYieldConsolidatedThreshold { get; init; }
	public required int NearROIInYears { get; init; }
	public required int MiddleROIInYears { get; init; }
	public required int LongROIInYears { get; init; }
	public required double RealROIThresholdNear { get; init; }
	public required double RealROIThresholdMiddle { get; init; }
	public required double RealROIThresholdLong { get; init; }
	public required double NominalROIThresholdNear { get; init; }
	public required double NominalROIThresholdMiddle { get; init; }
	public required double NominalROIThresholdLong { get; init; }
	public required int BazinYearAmount { get; init; }
	public required double DailyLiquidityThreshold { get; init; }
	public required int DividendYieldRecentAmount { get; init; }
	public required int DividendYieldConsolidatedAmount { get; init; }
}
