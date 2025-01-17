using HtmlAgilityPack;
using StockBrain.Infra.IndicatorGetters.Abstractions;
using StockBrain.Infra.IndicatorGetters.Abstractions.Investidor10;

namespace StockBrain.Infra.IndicatorGetters.Investidor10;

public class Investidor10IndicatorGetter : IIndicatorGetter
{
	private const string BaseUrl = "https://investidor10.com.br/acoes/";

	string GetURI(string ticker) => $"{BaseUrl}/{ticker}/";

	public async Task<IDictionary<Indicators, bool>> Get(string ticker)
	{
		using (HttpClient client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");

			HttpResponseMessage response = await client.GetAsync(GetURI(ticker));
			string html = await response.Content.ReadAsStringAsync();

			var document = new HtmlDocument();
			document.LoadHtml(html);

			var items = new Dictionary<Indicators, bool>();

			foreach (var criteria in Investidor10HtmlIndicatorMappers.ChecklistValues)
				items.Add(criteria.Key, document.GetElementbyId(criteria.Value).Attributes.Contains("checked"));

			return items;
		}
	}
}
