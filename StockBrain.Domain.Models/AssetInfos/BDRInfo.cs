using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.AssetInfos;

public class BDRInfo : AssetInfo
{
	public BDRInfo(string ticker)
	{
		Ticker = ticker;
	}

	public BDRInfo(Asset asset) : this(asset.Ticker)
	{

	}
	public BDRInfo()
	{

	}
	public double Price { get; set; }
	public double Equity { get; set; }
	public double ROE { get; set; }
	public double LPA { get; set; }
	public double VPA { get; set; }
	public bool HasNeverPostedLosses { get; set; }
	public bool ProfitableLastQuarters { get; set; }
	public bool WellRated { get; set; }

}
