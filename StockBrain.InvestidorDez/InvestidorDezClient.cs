using HtmlAgilityPack;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez;

public class InvestidorDezClient : IDisposable
{
	private const string BaseUrl = "https://investidor10.com.br";
	private const string BaseAPIUrl = $"{BaseUrl}/api";
	HttpClient Client { get; }
	public InvestidorDezClient()
	{
		Client = GetClient();
	}

	public async Task<InvestidorDezPage> GetPage(string ticker, AssetType type)
	{
		var document = await GetDocument(ticker, type);
		var page = new InvestidorDezPage(document, type);
		page.Dividends = await GetStockDividends(ticker, type, page.ID);
		page.Prices = await GetStockPrices(ticker, type, page.ID);
		return page;
	}
	async Task<IDictionary<int, double>> GetStockDividends(string ticker, AssetType type, long id)
	{
		var json = await GetResponse(GetStockDividendURI(ticker, type, id));
		var values = json.Deserialize<List<ValueYear>>();
		return values.Any() ? values.OrderByDescending(v => v.Year).ToDictionary(v => v.Year, v => v.Value) : new Dictionary<int, double>();
	}
	async Task<IDictionary<DateOnly, double>> GetStockPrices(string ticker, AssetType type, long id)
	{
		var json = await GetResponse(GetStockPriceURI(ticker, type, id));
		IEnumerable<ValueDate> prices = null;

		if (type == AssetType.Acoes)
			prices = json.Deserialize<InvestidorDezCotacoes>(new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy HH:mm" }).Real;
		else
			prices = json.Deserialize<List<ValueDate>>(new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy" });

		return prices.OrderByDescending(v => v.Date).ToDictionary(v => v.Date.ToDateOnly(), v => v.Value.ToPrecision(2));
	}

	async Task<string> GetResponse(string uri)
	{
		var response = await Client.GetAsync(uri);
		return await response.Content.ReadAsStringAsync();
	}
	async Task<HtmlDocument> GetDocument(string ticker, AssetType type)
	{
		var document = new HtmlDocument();
		document.LoadHtml(await GetResponse(GetDocumentURI(ticker, type)));
		return document;
	}
	HttpClient GetClient()
	{
		var client = new HttpClient();
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
		return client;
	}

	string GetDocumentURI(string ticker, AssetType type)
	{
		var typeUrl = type switch
		{
			AssetType.Acoes => "acoes",
			AssetType.BDR => "bdrs",
			AssetType.FII => "fiis",
			_ => ""
		};
		return $"{BaseUrl}/{typeUrl}/{ticker}/";
	}
	string GetStockDividendURI(string ticker, AssetType type, long id)
	{
		return type switch
		{
			AssetType.Acoes => $"{BaseAPIUrl}/dividendos/chart/{ticker}/1825/ano/",
			AssetType.BDR => $"{BaseAPIUrl}/bdr/dividend-yield/chart/{id}/1825/ano",
			AssetType.FII => "",
			_ => ""
		};

	}
	string GetStockPriceURI(string ticker, AssetType type, long id)
	{
		return type switch
		{
			AssetType.Acoes => $"{BaseAPIUrl}/cotacoes/acao/chart/{ticker}/365/true/real/",
			AssetType.BDR => $"{BaseAPIUrl}/bdr/cotacoes/chart/{id}/365/true",
			AssetType.FII => "",
			_ => ""
		};

	}

	public void Dispose()
	{
		Client.Dispose();
	}
}
