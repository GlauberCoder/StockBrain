namespace StockBrain.Domain.Models.EvaluationConfigs;

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
	public required double RealROIThresholdRecent { get; init; }
	public required double RealROIThresholdConsolidated { get; init; }
	public required double NominalROIThresholdRecent { get; init; }
	public required double NominalROIThresholdConsolidated { get; init; }
	public required int BazinYearAmount { get; init; }
	public required double DailyLiquidityThreshold { get; init; }
	public required int DividendYieldRecentAmount { get; init; }
	public required int DividendYieldConsolidatedAmount { get; init; }
}
