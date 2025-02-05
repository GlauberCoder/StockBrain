using HtmlAgilityPack;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public abstract class InvestidorRequester
{
	protected const string BaseUrl = "https://investidor10.com.br";
	protected const string BaseAPIUrl = $"{BaseUrl}/api";
	HttpClient Client { get; }
	Context Context { get; }

	public InvestidorRequester(HttpClient client, Context context)
	{
		Client = client;
		Context = context;
	}

	public async Task<InvestidorDezPage> GetPage(string ticker) => new InvestidorDezPage(await GetDocument(ticker), ticker, GetType(), this);
	public async Task<IDictionary<DateOnly, double>> GetDividends(string ticker, long id) => await GetTimeSeries(GetDividendURI(ticker, id), DeserializeDividends, 2, false);
	public async Task<IDictionary<DateOnly, double>> GetDividendYields(string ticker, long id) => await GetTimeSeries(GetDividendYieldURI(ticker, id), DeserializeDividendYields, 4, true);
	public async Task<IDictionary<DateOnly, double>> GetPrices(string ticker, long id) => await GetTimeSeries(GetPriceURI(ticker, id), DeserializePrices, 2, false);

	public async Task<IDictionary<DateOnly, double>> GetTimeSeries(string uri, Func<string, IEnumerable<ValueDate>> deserializer, int precision, bool toPercentual)
	{
		var json = await GetResponse(uri);
		var values = deserializer(json);
		return values.Any() ? values.OrderByDescending(v => v.Date).ToDictionary(v => v.Date.ToDateOnly(), v => (v.Value/ (toPercentual ? 100 : 1)).ToPrecision(precision)) : new Dictionary<DateOnly, double>();
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
	protected IEnumerable<ValueDate> DeserializeFromValueYear(string json) => json.Replace("Atual", Context.Today.Year.ToString()).Deserialize<List<ValueYear>>().Select(r => new ValueDate(r));
	protected abstract AssetType GetType();
	protected abstract IEnumerable<ValueDate> DeserializeDividends(string json);
	protected abstract IEnumerable<ValueDate> DeserializePrices(string json);
	protected abstract IEnumerable<ValueDate> DeserializeDividendYields(string json);
	protected abstract string GetDocumentURI(string ticker) ;
	protected abstract string GetDividendURI(string ticker, long id);
	protected abstract string GetDividendYieldURI(string ticker, long id);
	protected abstract string GetPriceURI(string ticker, long id);
}
