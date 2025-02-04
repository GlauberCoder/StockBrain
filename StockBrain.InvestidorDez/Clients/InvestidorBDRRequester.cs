﻿using Newtonsoft.Json;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Models;
using StockBrain.Utils;

namespace StockBrain.InvestidorDez.Clients;

public class InvestidorBDRRequester : InvestidorRequester
{
	public InvestidorBDRRequester(HttpClient client) : base(client)
	{
	}

	protected override IEnumerable<ValueDate> DeserializePrices(string json) => json.Deserialize<List<ValueDate>>(new Newtonsoft.Json.JsonSerializerSettings { DateFormatString = "dd/MM/yyyy" });
	protected override string GetDocumentURI(string ticker) => $"{BaseUrl}/bdrs/{ticker}/";
	protected override string GetDividendURI(string ticker, long id) => $"{BaseAPIUrl}/bdr/dividend-yield/chart/{id}/1825/ano";
	protected override string GetPriceURI(string ticker, long id) => $"{BaseAPIUrl}/bdr/cotacoes/chart/{id}/365/true";
	protected override AssetType GetType() => AssetType.BDR;

	protected override IEnumerable<ValueDate> DeserializeDividends(string json)
	{
		return json.Deserialize<List<ValueYear>>().Select(r => new ValueDate { Value = r.Value, Date = new DateTime(r.Year, 1, 1) });
	}
}
