using Newtonsoft.Json;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public class InvestidorStockRequester : InvestidorRequester
{
	public InvestidorStockRequester(HttpClient client, Context context) : base(client, context)
	{
	}

	protected override IEnumerable<ValueDate> DeserializePrices(string json) => json.Deserialize<InvestidorDezCotacoes>(new JsonSerializerSettings { DateFormatString = "dd/MM/yyyy HH:mm" }).Real;
	protected override string GetDocumentURI(string ticker) => $"{BaseUrl}/acoes/{ticker}/";
	protected override string GetDividendURI(string ticker, long id) => $"{BaseAPIUrl}/dividendos/chart/{ticker}/1825/ano/";
	protected override string GetPriceURI(string ticker, long id) => $"{BaseAPIUrl}/cotacoes/acao/chart/{ticker}/365/true/real/";
	protected override string GetDividendYieldURI(string ticker, long id) => $"{BaseAPIUrl}/dividend-yield/chart/{ticker}/1825/mes/?v=2";
	protected override AssetType GetType()=> AssetType.Acoes;

	protected override IEnumerable<ValueDate> DeserializeDividends(string json) => DeserializeFromValueYear(json);
	protected override IEnumerable<ValueDate> DeserializeDividendYields(string json) => DeserializeFromValueYear(json);
}
