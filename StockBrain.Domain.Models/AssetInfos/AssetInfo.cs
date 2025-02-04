using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.AssetInfos;

public abstract class AssetInfo : BaseEntity
{
	public AssetInfo()
	{

	}
	public string Ticker { get; set; }
	public IDictionary<DateOnly, double> Prices { get; set; }
	public IDictionary<DateOnly, double> Dividends { get; set; }

}
