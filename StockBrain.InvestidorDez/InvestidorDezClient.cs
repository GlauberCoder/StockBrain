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
		var dividends = await GetStockDividends(ticker);
		var prices = await GetStockPrices(ticker);
		return new InvestidorDezPage(document, dividends, prices);
	}
	async Task<IDictionary<int, double>> GetStockDividends(string ticker)
	{
		var json = await GetResponse(GetStockDividendURI(ticker));
		var values = json.Deserialize<List<ValueYear>>();
		return values.OrderByDescending(v => v.Year).ToDictionary(v => v.Year, v => v.Value);
	}
	async Task<IDictionary<DateOnly, double>> GetStockPrices(string ticker)
	{
		var json = await GetResponse(GetStockPriceURI(ticker));
		var result = json.Deserialize<InvestidorDezCotacoes>(new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy HH:mm" });
		return result.Real.OrderByDescending(v => v.Date).ToDictionary(v => v.Date.ToDateOnly(), v => v.Value.ToPrecision(2));
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
	string GetStockDividendURI(string ticker) => $"{BaseAPIUrl}/dividendos/chart/{ticker}/1825/ano/";
	string GetStockPriceURI(string ticker) => $"{BaseAPIUrl}/cotacoes/acao/chart/{ticker}/365/true/real/";

	public void Dispose()
	{
		Client.Dispose();
	}
}
