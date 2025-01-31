using HtmlAgilityPack;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Portfolio;

public class GetIndicators
{
	public string GetCotacoes(int id, int timestamp)
		=> $"https://investidor10.com.br/api/fii/cotacoes/chart/{id}/{timestamp}/real/adjusted/true";

	public string GetDividend(int id, int timestamp)
		=> $"https://investidor10.com.br/api/fii/dividendos/chart/{id}/{timestamp}/mes";

	public async Task Build(string tick)
	{

		var url = $"https://investidor10.com.br/fiis/{tick}/";

		using (HttpClient client = new HttpClient())
		{
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");

			HttpResponseMessage response = await client.GetAsync(url);
			string html = await response.Content.ReadAsStringAsync();

			var document = new HtmlDocument();
			document.LoadHtml(html);
			var id = GetID(document);

			var urlCotacoes = GetCotacoes(id, 365);
			HttpResponseMessage responsecotacoes = await client.GetAsync(urlCotacoes);
			string htmlCotacoes = await responsecotacoes.Content.ReadAsStringAsync();
			var values = JsonSerializer.Deserialize<Dictionary<string, List<object>>>(htmlCotacoes);

			var urlDivind = GetDividend(id, 365 * 5);
			HttpResponseMessage responseDiviend = await client.GetAsync(urlDivind);
			string htmlDividend = await responseDiviend.Content.ReadAsStringAsync();
			var dividendValue = JsonSerializer.Deserialize<List<object>>(htmlDividend);

			//document.DocumentNode.Line
			var dividendYield = GetStockValueTopValuesa(document, FIIStats.DividendYield);
			var pvp = GetStockValueTopValuesa(document, FIIStats.PVP);
			var liquidity = GetStockValueTopValuesa(document, FIIStats.Liquidity);
			var price = GetStockValueTopValuesa(document, FIIStats.Price);
			var tableValues = GetStockTableValues(document);
			var returnValues = GetReturnInvestment(document);
			//var child = nodes.First().SelectSingleNode("//div[contains(@class, 'value')]");
		}
	}

	int GetID(HtmlDocument html)
	{
		var nodes = html.DocumentNode.Descendants("script");
		foreach (var node in nodes)
		{
			var scriptInner = node.InnerHtml;
			if (scriptInner.Contains("var tickerToCompare"))
			{
				var firstLine = scriptInner.Split("\n").ElementAt(1).Trim();
				var indexOf = firstLine.IndexOf("{");
				var substring = firstLine.Substring(indexOf, firstLine.Length - indexOf - 4);
				var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(substring);
				return int.Parse(obj["id"].ToString());
			}
		}
		return 1;
		//'var tickerToCompare = [JSON.parse(`{ { ""ticker"":""ALZR11"",""type"":""fii"",""id"":20} }`)];'
	}

	decimal ConvertTo(string value)
	{
		CultureInfo ptBRCulture = new CultureInfo("pt-BR");
		return decimal.Parse(value, ptBRCulture);
	}

	decimal GetStockValueTopValuesa(HtmlDocument document, FIIStats fIIStats)
	{
		var statsKey = FFIStatsTranslation[fIIStats];
		var regex = new Regex("\\d+[\\.,]?\\d*");
		var nodes = document.DocumentNode.SelectSingleNode($"//div[@class='_card {statsKey}']").SelectSingleNode(".//div[@class='_card-body']").SelectSingleNode(".//span").InnerHtml;
		var match = regex.Match(nodes).Value;
		return ConvertTo(match);
	}

	Dictionary<string, string> GetReturnInvestment(HtmlDocument document)
	{
		var nodes = document.DocumentNode.SelectSingleNode(".//div[contains(@class,'return-investment')]").SelectNodes(".//div[@class='result-period']");
		var dictionary = new Dictionary<string, string>();
		foreach (var node in nodes.Take(6))
		{
			var desc = node.SelectSingleNode(".//h4").InnerHtml.Trim();
			var value = node.SelectSingleNode(".//span").InnerHtml.Trim();
			dictionary.Add(desc, value);
		}
		return dictionary;
	}

	Dictionary<string, string> GetStockTableValues(HtmlDocument document)
	{
		var regex = new Regex("\\d+[\\.,]?\\d*");
		var dictionary = new Dictionary<string, string>();
		var nodes = document.DocumentNode.SelectSingleNode(".//div[@id='table-indicators']").SelectNodes(".//div[@class='cell']");
		foreach (var node in nodes)
		{
			var desc = node.SelectSingleNode(".//div[@class='desc']").SelectSingleNode(".//span").InnerHtml.Trim();
			var value = node.SelectSingleNode(".//div[@class='desc']").SelectSingleNode(".//div").SelectSingleNode(".//span").InnerHtml.Trim();
			dictionary.Add(desc, value);
		}
		return dictionary;
	}


	IDictionary<FIIStats, string> FFIStatsTranslation = new Dictionary<FIIStats, string>
	{
		{ FIIStats.DividendYield, "dy" },
		{ FIIStats.PVP, "vp" },
		{ FIIStats.Liquidity, "val" },
		{ FIIStats.Price, "cotacao" },
	};

}
enum FIIStats
{
	DividendYield,
	PVP,
	AssetValue,
	Liquidity,
	Price,
}