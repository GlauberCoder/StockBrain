﻿using Microsoft.Extensions.DependencyInjection;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.JSONFiles;
using StockBrain.Services;
using StockBrain.Services.Abstrations;
using StockBrain.Utils;

namespace StockBrain.Playground.CMD;

internal class Program
{
	static ServiceProvider ServiceProvider { get; set; }
	static long PortifolioID { get; set; } = 1;
	static void Main(string[] args)
	{
		BuildServices();
		string option = string.Empty;

		while (option != "E")
		{
			option = PrintOptions();
			RunOption(option);
		}
	}
	private static T GetService<T>() => ServiceProvider.GetService<T>();
	static void RunOption(string option)
	{
		Console.Clear();
		switch (option)
		{
			case "1":
				ListAssets();
				break;
			case "2":
				UpdateAllPrices();
				break;
			case "3":
				ListNullPriceTickers();
				break;
			case "4":
				UpdateMissingPrices();
				break;
			case "5":
				ListBonds();
				break;
			case "6":
				ListPortifolioAssets();
				break;
			case "7":
				ChangePortifolio();
				break;
		}
		Console.ReadKey();
	}
	static string PrintOptions()
	{
		Console.Clear();
		Console.WriteLine($"Portifólio {PortifolioID}");
		Console.WriteLine("Seleciona uma opção");
		Console.WriteLine("1 - Listar ativos");
		Console.WriteLine("2 - Atualizar todos os preços");
		Console.WriteLine("3 - Listar preços vazios");
		Console.WriteLine("4 - Atualizar preços vazios");
		Console.WriteLine("5 - Listar Renda Fixa");
		Console.WriteLine("6 - Listar Assets do portifólio");
		Console.WriteLine("7 - Change Portifólio");
		Console.WriteLine("E - Sair");
		return Console.ReadLine();
	}
	static void ChangePortifolio()
	{
		Console.WriteLine("Insira o ID do Portifólio:");
		PortifolioID = long.Parse(Console.ReadLine());
	}
	static void ListAssets()
	{
		foreach (var asset in GetService<IAssets>().All())
			PrintAsset(asset);
	}
	static void ListBonds()
	{
		var bounds = GetService<IBonds>().ByPortifolio(PortifolioID);
		foreach (var bond in bounds)
			PrintBond(bond);


		Console.WriteLine($"Total: {bounds.Where(b => !b.Expired && !b.Redeemed).Sum(b => b.Value)}");
	}
	static void ListPortifolioAssets()
	{
		var assets = GetService<IPortfolioAssets>().ByPortifolio(PortifolioID);
		foreach (var asset in assets)
			PrintPortfolioAsset(asset);
		var delta = new DeltaValue(assets.Sum(b => b.InvestedValue), assets.Sum(a => a.CurrentValue));
		Console.WriteLine($"Investment: {delta.Initial.ToPrecision()} Current: {delta.Final.ToPrecision()} Difference: {delta.Difference.ToPrecision()} ({(delta.Percentage * 100).ToPrecision()}%)");
	}
	static void UpdateAllPrices()
	{
		Task.WaitAll(GetService<IPriceUpdater>().UpdateAll());
		ListNullPriceTickers();
		Console.WriteLine("Atualizado");
	}
	static void UpdateMissingPrices()
	{
		Task.WaitAll(GetService<IPriceUpdater>().UpdateMissing());
		ListNullPriceTickers();
		Console.WriteLine("Atualizado");
	}
	static void ListNullPriceTickers()
	{
		foreach (var asset in GetService<IAssets>().All().Where(a => !a.MarketPrice.HasValue))
			Console.WriteLine($"{asset.Ticker} Not Found");
	}
	private static void BuildServices()
	{
		ServiceProvider = new ServiceCollection()
			.AddScoped(sp => new BrAPIConfig { ApiKey = "2MVc6qfPniXFuAaDyMnFDf" })
			.AddScoped(sp => new Context { Account = new Account { GUID = Guid.NewGuid().ToString(), ID = 1, Name = "Glauber" } })
			.AddScoped(sp => new DataJSONFilesConfig { BasePath = "C:\\Dev\\StockBrain" })
					.AddScoped<IAccounts, Accounts>()
					.AddScoped<IAssets, Assets>()
					.AddScoped<ISectors, Sectors>()
					.AddScoped<ISegments, Segments>()
					.AddScoped<IBrokers, Brokers>()
					.AddScoped<IBondIssuers, BondIssuers>()
					.AddScoped<IBonds, Bonds>()
					.AddScoped<IDecisionFactors, DecisionFactors>()
					.AddScoped<IAssetDecisionFactors, AssetDecisionFactors>()
					.AddScoped<IPortfolioAssets, PortfolioAssets>()
					.AddScoped<IPortfolios, Portfolios>()
					.AddScoped<IPriceGetter, BrAPIMarketPriceGetter>()
					.AddScoped<IPriceUpdater, PriceUpdater>()
			.BuildServiceProvider();
	}
	private static string WriteYearAndMonths(TimeSpan span)
	{
		var years = span.Years();
		var months = span.Months();
		var yearText = years > 0 ? (years > 1 ? $"{years} anos" : $"{years} ano") : string.Empty;
		var monthText = months > 0 ? (months > 1 ? $"{months} meses" : $"{months} mês") : string.Empty;

		return yearText + (string.IsNullOrEmpty(yearText) || string.IsNullOrEmpty(monthText) ? monthText : $" e {monthText}");
	}
	private static void PrintPortfolioAsset(PortfolioAsset asset)
	{
		PrintAsset(asset.Asset);
		Console.WriteLine($"Quantity: {asset.Quantity}");
		Console.WriteLine($"Avg Price: {asset.AveragePrice}");
		Console.WriteLine($"Value: {asset.InvestedValue}");
		Console.WriteLine($"Current Value: {asset.CurrentValue}");
		Console.WriteLine($"Risk: {(asset.Risk ? "Yes" : "NO")}");
		Console.WriteLine($"Start: {asset.FirstAquisition}");
		Console.WriteLine($"Last: {asset.LastAquisition}");
		Console.WriteLine($"Price: {asset.DeltaPrice.Difference.ToPrecision()}");
		Console.WriteLine($"Total: {asset.DeltaTotal.Difference.ToPrecision()}");
		Console.WriteLine($"Evolution: {(asset.DeltaTotal.Percentage * 100).ToPrecision()}%");
		Console.WriteLine("============================================");
		Console.WriteLine();
	}
	private static void PrintBond(Bond bond)
	{
		Console.WriteLine($"[{bond.ID}][{bond.Type.ToString()}] {bond.Issuer.Name} {bond.Index.ToString()} {bond.Tax}%");
		Console.WriteLine("============================================");
		Console.WriteLine($"Valor: {bond.Value}");
		Console.WriteLine($"Tipo: {bond.Type}");
		Console.WriteLine($"Index: {bond.Index}");
		Console.WriteLine($"Taxa: {bond.Tax}");
		Console.WriteLine($"Emissor: {bond.Issuer.Name}");
		Console.WriteLine($"Corretora: {bond.Broker.Name}");
		Console.WriteLine($"Data: {bond.Date}");
		Console.WriteLine($"Expiracao: {bond.Expiration}");
		Console.WriteLine($"Age: {WriteYearAndMonths(bond.Age)}");
		Console.WriteLine($"Time to expire: {(bond.Expired ? "Expired" : WriteYearAndMonths(bond.TimeToExpire))}");
		Console.WriteLine("============================================");
		Console.WriteLine();
	}
	private static void PrintAsset(Asset asset)
	{
		Console.WriteLine($"Ticker: {asset.Ticker}");
		Console.WriteLine("============================================");
		Console.WriteLine($"Name: {asset.Name}");
		Console.WriteLine($"Description: {asset.Description}");
		Console.WriteLine($"Sector: {asset.Sector.Name}");
		Console.WriteLine($"Segment: {asset.Segment.Name}");
		Console.WriteLine($"IPO: {asset.IPO}");
		Console.WriteLine($"Foundation: {asset.Foundation}");
		Console.WriteLine($"Positive: {asset.PositiveNotes}");
		Console.WriteLine($"Negative: {asset.NegativeNotes}");
		Console.WriteLine($"Age: {WriteYearAndMonths(asset.Foundation.Span)}");
		Console.WriteLine($"IPO Time: {WriteYearAndMonths(asset.IPO.Span)}");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"Price: {asset.MarketPrice}");
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.WriteLine("============================================");
		Console.WriteLine();
	}
}
