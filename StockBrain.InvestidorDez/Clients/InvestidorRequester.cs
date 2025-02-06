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
		return values.Any() ? values.GroupBy(v => v.Date.ToDateOnly()).OrderByDescending(v => v.Key).ToDictionary(v => v.Key, v => (v.Sum(s => s.Value)/ (toPercentual ? 100 : 1)).ToPrecision(precision)) : new Dictionary<DateOnly, double>();
	}

	async Task<string> GetResponse(string uri)
	{
		var response = Client.GetAsync(uri).Result;
		return response.Content.ReadAsStringAsync().Result;
	}
	public async Task<HtmlDocument> GetDocument(string ticker)
	{
		var document = new HtmlDocument();
		var html = GetResponse(GetDocumentURI(ticker)).Result;
		document.LoadHtml(html);
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
