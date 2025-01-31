using StockBrain.Domain.Models;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez;

public class InvestidorDezAssetInfoGetter : IAssetInfoGetter
{
	private StockInfoMap StockMap = new StockInfoMap();
	public async Task<StockInfo> GetStock(Asset asset) => (await GetStock(new List<Asset> { asset })).First();
	public async Task<IEnumerable<StockInfo>> GetStock(IEnumerable<Asset> assets)
	{
		using var client = GetClient();
		var results = new List<StockInfo>();

		foreach(var asset in assets)
			results.Add(await GetStock(asset, client));

		return results;
	}

	async Task<StockInfo> GetStock(Asset asset, InvestidorDezClient client) 
	{
		var result = new StockInfo(asset.Ticker);
		var page = await GetPage(asset, client);
		StockMap.Set(result, page);
		result.Dividends = page.Dividends;
		result.Prices = page.Prices;
		return result;
	}

	async Task<InvestidorDezPage> GetPage(Asset asset, InvestidorDezClient client) => await client.GetPage(asset.Ticker, asset.Type);
	InvestidorDezClient GetClient() => new InvestidorDezClient();

}
