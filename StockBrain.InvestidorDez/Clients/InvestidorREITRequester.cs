using HtmlAgilityPack;
using Newtonsoft.Json;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public class InvestidorREITRequester : InvestidorRequester
{
	public InvestidorREITRequester(HttpClient client, Context context) : base(client, context)
	{
	}

	protected override IEnumerable<ValueDate> DeserializePrices(string json) => json.Deserialize<InvestidorDezCotacoes>(new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy" }).Real;
	protected override string GetDocumentURI(string ticker) => $"{BaseUrl}/fiis/{ticker}/";
	protected override string GetDividendURI(string ticker, long id) => $"{BaseAPIUrl}/fii/dividendos/chart/{id}/1825/mes";
	protected override string GetPriceURI(string ticker, long id) => $"{BaseAPIUrl}/fii/cotacoes/chart/{id}/365/real/adjusted/true";
	protected override string GetDividendYieldURI(string ticker, long id) => $"{BaseAPIUrl}/fii/dividend-yield/chart/{id}/1825/mes";
	protected override AssetType GetType() => AssetType.FII;
	protected override IEnumerable<ValueDate> DeserializeDividends(string json) => json.Deserialize<List<ValueDate>>(new JsonSerializerSettings { DateFormatString = "01/MM/yyyy" });
	protected override IEnumerable<ValueDate> DeserializeDividendYields(string json) => json.Deserialize<List<ValueDate>>(new JsonSerializerSettings { DateFormatString = "01/MM/yyyy" });

}
