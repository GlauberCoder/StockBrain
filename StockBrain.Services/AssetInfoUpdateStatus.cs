using StockBrain.Services.Abstrations;

namespace StockBrain.Services;

public class AssetInfoUpdateStatus : IAssetInfoUpdateStatus
{
	public AssetInfoUpdateStatus(string ticker)
	{
		Ticker = ticker;
	}
	public string Ticker { get; }
	public bool Done { get; set; }
	public bool HasError { get; set; }
	public string ErrorMessage { get; set; }
}
