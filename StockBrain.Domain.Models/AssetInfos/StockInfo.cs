using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.AssetInfos;

public class StockInfo : AssetInfo
{
    public StockInfo()
    {

    }
    public double Price { get; set; }
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

}
