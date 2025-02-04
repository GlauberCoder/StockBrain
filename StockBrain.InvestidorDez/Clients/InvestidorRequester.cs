using HtmlAgilityPack;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public abstract class InvestidorRequester
{
	protected const string BaseUrl = "https://investidor10.com.br";
	protected const string BaseAPIUrl = $"{BaseUrl}/api";
	HttpClient Client { get; }
	public InvestidorRequester(HttpClient client)
	{
		Client = client;
	}

	public async Task<InvestidorDezPage> GetPage(string ticker) => new InvestidorDezPage(await GetDocument(ticker), ticker, GetType(), this);
	public async Task<IDictionary<DateOnly, double>> GetDividends(string ticker, long id)
	{
		var json = await GetResponse(GetDividendURI(ticker, id));
		var values = DeserializeDividends(json);
		return values.Any() ? values.OrderByDescending(v => v.Date).ToDictionary(v => v.Date.ToDateOnly(), v => v.Value) : new Dictionary<DateOnly, double>();
	}
	public async Task<IDictionary<DateOnly, double>> GetPrices(string ticker, long id)
	{
		return DeserializePrices((await GetResponse(GetPriceURI(ticker, id)))).OrderByDescending(v => v.Date).ToDictionary(v => v.Date.ToDateOnly(), v => v.Value.ToPrecision(2));
	}

	async Task<string> GetResponse(string uri)
	{
		var response = await Client.GetAsync(uri);
		return await response.Content.ReadAsStringAsync();
	}
	public async Task<HtmlDocument> GetDocument(string ticker)
	{
		var document = new HtmlDocument();
		document.LoadHtml(await GetResponse(GetDocumentURI(ticker)));
		return document;
	}
	protected abstract AssetType GetType();
	protected abstract IEnumerable<ValueDate> DeserializeDividends(string json);
	protected abstract IEnumerable<ValueDate> DeserializePrices(string json);
	protected abstract string GetDocumentURI(string ticker) ;
	protected abstract string GetDividendURI(string ticker, long id);
	protected abstract string GetPriceURI(string ticker, long id);
}
