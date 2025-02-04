using Newtonsoft.Json;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public class InvestidorStockRequester : InvestidorRequester
{
	public InvestidorStockRequester(HttpClient client) : base(client)
	{
	}

	protected override IEnumerable<ValueDate> DeserializePrices(string json) => json.Deserialize<InvestidorDezCotacoes>(new JsonSerializerSettings { DateFormatString = "dd/MM/yyyy HH:mm" }).Real;
	protected override string GetDocumentURI(string ticker) => $"{BaseUrl}/acoes/{ticker}/";
	protected override string GetDividendURI(string ticker, long id) => $"{BaseAPIUrl}/dividendos/chart/{ticker}/1825/ano/";
	protected override string GetPriceURI(string ticker, long id) => $"{BaseAPIUrl}/cotacoes/acao/chart/{ticker}/365/true/real/";
	protected override AssetType GetType()=> AssetType.Acoes;

	protected override IEnumerable<ValueDate> DeserializeDividends(string json)
	{
		return json.Deserialize<List<ValueYear>>().Select(r => new ValueDate { Value = r.Value, Date = new DateTime(r.Year,1, 1) });
	}
}
