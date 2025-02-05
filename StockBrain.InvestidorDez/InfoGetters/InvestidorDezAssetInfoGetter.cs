using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez.InfoGetters;

public abstract class InvestidorDezAssetInfoGetter<TInfo, TMap> : IAssetInfoGetter<TInfo>
	where TInfo : AssetInfo, new()
	where TMap : AssetInfoMap<TInfo>, new()
{
	Context Context { get; }

	public InvestidorDezAssetInfoGetter(Context context)
	{
		Context = context;
	}
	public async Task<TInfo> Get(Asset asset) => (await Get(new List<Asset> { asset })).First();
	public async Task<IEnumerable<TInfo>> Get(IEnumerable<Asset> assets)
	{
		using var client = GetClient();
		var results = new List<TInfo>();

		foreach (var asset in assets)
			results.Add(await GetInfo(asset, client));

		return results;
	}

	async Task<TInfo> GetInfo(Asset asset, InvestidorDezClient client)
	{
		var result = new TInfo();
		result.Ticker = asset.Ticker;
		var page = await GetPage(asset, client);
		new TMap().Set(result, page);
		result.Dividends = page.Dividends;
		result.Prices = page.Prices;
		result.DividendYields = page.DividendYields;
		return OnGetInfoFinish(result, page, client);
	}
	async Task<InvestidorDezPage> GetPage(Asset asset, InvestidorDezClient client) => await client.GetPage(asset.Ticker, asset.Type);
	InvestidorDezClient GetClient() => new InvestidorDezClient(Context);
	protected virtual TInfo OnGetInfoFinish(TInfo info, InvestidorDezPage page, InvestidorDezClient client) => info;

}
