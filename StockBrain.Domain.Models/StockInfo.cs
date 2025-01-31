using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class StockInfo
{
	public StockInfo(string ticker)
	{
		Ticker = ticker;
	}

	public StockInfo(Asset asset) : this(asset.Ticker)
	{

	}
	public string Ticker { get; }
	public IDictionary<DateOnly, double> Prices { get; set; }
	public IDictionary<int, double> Dividends { get; set; }
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
