using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Utils;

namespace StockBrain.Infra.PriceGetters.BrAPI;

/// <summary>
/// https://brapi.dev/docs
/// </summary>
public class BrAPIMarketPriceGetter : IPriceGetter
{
	private const string BaseUrl = "https://brapi.dev/api/quote";
	BrAPIConfig Config { get; }

	public BrAPIMarketPriceGetter(BrAPIConfig config)
	{
		Config = config;
	}
	string GetURI(string ticker) => $"{BaseUrl}/{ticker}?token={Config.ApiKey}";
	public async Task<double?> Get(string ticker)
	{
		using var client = new HttpClient();
		var uri = GetURI(ticker);
		var json = await client.GetStringAsync(uri);
		var result = json.Deserialize<BrAPIMarketData>();
		double? price = result.Results.FirstOrDefault()?.RegularMarketPrice;

		return price;

	}


}
