﻿using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.AssetInfos;

public class StockInfo : AssetInfo
{
	public StockInfo()
	{

	}
	public double Price { get; set; }
	public double PL { get; set; }
	public double PVP { get; set; }
	public double Debt { get; set; }
	public double Equity { get; set; }
	public double ROE { get; set; }
	public double LPA { get; set; }
	public double VPA { get; set; }
	public double RevenueCAGR { get; set; }
	public double ProfitCAGR { get; set; }
	public double DailyLiquidity { get; set; }
	public bool HasNeverPostedLosses { get; set; }
	public bool ProfitableLastQuarters { get; set; }
	public bool PaidAcceptableDividends { get; set; }
	public bool WellRated { get; set; }
	public double RealROINear { get; set; }
	public double RealROIMiddle { get; set; }
	public double RealROILong { get; set; }
	public double NominalROINear { get; set; }
	public double NominalROIMiddle { get; set; }
	public double NominalROILong { get; set; }

}
