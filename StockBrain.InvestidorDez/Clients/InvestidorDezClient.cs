﻿using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.InvestidorDez.Clients;

public class InvestidorDezClient : IDisposable
{
	private const string BaseUrl = "https://investidor10.com.br";
	private const string BaseAPIUrl = $"{BaseUrl}/api";
	HttpClient Client { get; }
	Context Context { get; }

	public InvestidorDezClient(Context context)
	{
		Client = GetClient();
		Context = context;
	}

	public async Task<InvestidorDezPage> GetPage(string ticker, AssetType type)
	{
		var requester = GetRequester(type);
		var document = requester.GetDocument(ticker).Result;
		var page = new InvestidorDezPage(document, ticker, type, requester);
		return page;
	}
	InvestidorRequester GetRequester(AssetType type)
	{
		return type switch
		{
			AssetType.Acoes => new InvestidorStockRequester(Client, Context),
			AssetType.BDR => new InvestidorBDRRequester(Client, Context),
			AssetType.FII => new InvestidorREITRequester(Client, Context),
			_ => null
		};
	}

	HttpClient GetClient()
	{
		var client = new HttpClient();
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
		return client;
	}

	public void Dispose()
	{
		Client.Dispose();
	}
}
