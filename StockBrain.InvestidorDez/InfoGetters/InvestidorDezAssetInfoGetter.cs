using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez.InfoGetters;

public abstract class InvestidorDezAssetInfoGetter<TInfo> : IAssetInfoGetter<TInfo>
	where TInfo : AssetInfo, new()
{
	Context Context { get; }
	InvestidorDezClient Client { get; }

	public InvestidorDezAssetInfoGetter(Context context, InvestidorDezClient client)
	{
		Context = context;
		Client = client;
	}
	public async Task<IEnumerable<TInfo>> Get(IEnumerable<Asset> assets)
	{
		var results = new List<TInfo>();

		foreach (var asset in assets)
			results.Add(await Get(asset));

		return results;
	}

	public async Task<TInfo> Get(Asset asset)
	{
		var result = new TInfo();
		result.Ticker = asset.Ticker;
		var page = await GetPage(asset);
		GetMap().Set(result, page);
		result.Dividends = page.Dividends;
		result.Prices = page.Prices;
		result.DividendYields = page.DividendYields;
		return OnGetInfoFinish(result, page, Client);
	}
	async Task<InvestidorDezPage> GetPage(Asset asset) => await Client.GetPage(asset.Ticker, asset.Type);
	protected virtual TInfo OnGetInfoFinish(TInfo info, InvestidorDezPage page, InvestidorDezClient client) => info;
	protected abstract AssetInfoMap<TInfo> GetMap();

}
