using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.AssetInfos;

public class REITInfo : AssetInfo
{
	public REITInfo()
	{

	}
	public double Price { get; set; }
	public double PVP { get; set; }
	public double DailyLiquidity { get; set; }
	public double RealROINear { get; set; }
	public double RealROIMiddle { get; set; }
	public double RealROILong { get; set; }
	public double NominalROINear { get; set; }
	public double NominalROIMiddle { get; set; }
	public double NominalROILong { get; set; }
	public double ManagementFee { get; set; }
	public double VacancyRate { get; set; }
	public double AssetValue { get; set; }
	public bool WellRated { get; set; }
	public int RegionCount { get; set; }
	public int PropertyCount { get; set; }

}
