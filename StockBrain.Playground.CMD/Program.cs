using Microsoft.Extensions.DependencyInjection;
using StockBrain.Domain;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.JSONFiles;
using StockBrain.InvestidorDez;
using StockBrain.InvestidorDez.InfoGetters;
using StockBrain.Services;
using StockBrain.Services.Abstrations;
using StockBrain.Utils;

namespace StockBrain.Playground.CMD;

internal class Program
{
	static ServiceProvider ServiceProvider { get; set; }
	static long PortifolioID { get; set; } = 1;
	static async Task Main(string[] args)
	{
		BuildServices();
		//string option = string.Empty;

		//while (option != "E")
		//{
		//	option = PrintOptions();
		//	RunOption(option);
		//}
		//	printInfo(GetStockInfo("FLRY3"));
		//	printInfo(GetBDRInfo("ROXO34"));

		//CreateREITInfo("HGLG11");
		//PrintREITEvaluation("HGLG11");

		//CreateBDRInfo("ROXO34");
		//PrintBDREvaluation("ROXO34");

		//CreateStockInfo("FLRY3");
		//PrintEvaluation("FLRY3", "ROXO34", "HGLG11");
		CreateInfos("XPML11");



	}
	private static void PrintEvaluation(params string[] tickers)
	{
		var assets = GetService<IPortfolioAssets>().ByPortifolio(2).Where(p => tickers.Contains(p.Asset.Ticker));
		foreach (var asset in assets)
		{
			Console.WriteLine($"{asset.Asset.Ticker}");
			if (asset.Score != null)
			{
				Console.WriteLine($"Score: {asset.Score.Proportion.PercentageFormat()} {asset.Score.Value}/{asset.Score.Total}");

				foreach (var answer in asset.Answers)
					Console.WriteLine($"{answer.Factor.Name} {answer.Answer}");
			}
			Console.WriteLine($"========================================================");
		}
	}
	static async Task CreateInfos(params string[] tickers)
	{
		await GetService<IAssetInfoUpdater>().UpdateAll(null, tickers);
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
	static void printStats(StockStats asset)
	{
		Console.WriteLine($"DY AVG: {asset.DividendAVG}");
		Console.WriteLine($"Bazin: {asset.BazinPrice}");
		Console.WriteLine($"Graham: {asset.GrahamPrice}");
		Console.WriteLine($"Fast AVG: {asset.FastAvg}");
		Console.WriteLine($"Slow AVG: {asset.SlowAvg}");
		Console.WriteLine($"Down Trend: {asset.DownTrend}");
		Console.WriteLine($"Has Liquidity: {asset.HasLiquidity}");
		Console.WriteLine($"Low Debt To Equity: {asset.LowDebtToEquity}");
		Console.WriteLine($"HasAcceptable ROE: {asset.HasAcceptableROE}");
		Console.WriteLine($"Positive Revenue CAGR: {asset.PositiveRevenueCAGR}");
		Console.WriteLine($"Positive Profit CAGR: {asset.PositiveProfitCAGR}");
	}
	static void printInfo(StockInfo asset)
	{
		Console.WriteLine($"Ticker: {asset.Ticker}");
		Console.WriteLine($"HasNeverPostedLosses: {asset.HasNeverPostedLosses}");
		Console.WriteLine($"ProfitableLastQuarters: {asset.ProfitableLastQuarters}");
		Console.WriteLine($"PaidAcceptableDividends: {asset.PaidAcceptableDividends}");
		Console.WriteLine($"WellRated: {asset.WellRated}");
		Console.WriteLine($"Price: {asset.Price}");
		Console.WriteLine($"Divida: {asset.Debt}");
		Console.WriteLine($"ROE: {asset.ROE}");
		Console.WriteLine($"LPA: {asset.LPA}");
		Console.WriteLine($"VPA: {asset.VPA}");
		Console.WriteLine($"Revenue CAGR: {asset.RevenueCAGR}");
		Console.WriteLine($"Profit CAGR: {asset.ProfitCAGR}");
		Console.WriteLine($"Daily Liquidity: {asset.DailyLiquidity}");
		Console.WriteLine($"Patrimonio: {asset.Equity}");

		foreach (var dividend in asset.Dividends)
			Console.WriteLine($"{dividend.Key}: {dividend.Value}");


		foreach (var price in asset.Prices.Take(10))
			Console.WriteLine($"{price.Key}: {price.Value}");
	}
	static void printInfo(REITInfo asset)
	{
		Console.WriteLine($"Ticker: {asset.Ticker}");
		Console.WriteLine($"Price: {asset.Price}");
		Console.WriteLine($"P/VP: {asset.PVP}");
		Console.WriteLine($"Liquidez Diária: {asset.DailyLiquidity}");
		Console.WriteLine($"ROI 5y: {asset.NominalROINear}");
		Console.WriteLine($"ROI 10y: {asset.NominalROILong}");
		Console.WriteLine($"ROI Real 5y: {asset.RealROINear}");
		Console.WriteLine($"ROI Real 10y: {asset.RealROILong}");
		Console.WriteLine($"Taxa de gestão: {asset.ManagementFee}");
		Console.WriteLine($"Taxa de vacância: {asset.VacancyRate}");
		Console.WriteLine($"Patrimônio: {asset.AssetValue}");
		Console.WriteLine($"Well Rated: {asset.WellRated}");
		Console.WriteLine($"Regions: {asset.RegionCount}");
		Console.WriteLine($"Properties: {asset.PropertyCount}");

		//foreach (var dividend in asset.Dividends)
		//	Console.WriteLine($"{dividend.Key}: {dividend.Value}");


		//foreach (var price in asset.Prices.Take(10))
		//	Console.WriteLine($"{price.Key}: {price.Value}");

		//if(asset.DividendYields != null)
		//	foreach (var price in asset.DividendYields.Take(10))
		//		Console.WriteLine($"{price.Key}: {price.Value}");
	}
	static void printInfo(BDRInfo asset)
	{
		Console.WriteLine($"Ticker: {asset.Ticker}");
		Console.WriteLine($"HasNeverPostedLosses: {asset.HasNeverPostedLosses}");
		Console.WriteLine($"ProfitableLastQuarters: {asset.ProfitableLastQuarters}");
		Console.WriteLine($"WellRated: {asset.WellRated}");
		Console.WriteLine($"Price: {asset.Price}");
		Console.WriteLine($"ROE: {asset.ROE}");
		Console.WriteLine($"LPA: {asset.LPA}");
		Console.WriteLine($"VPA: {asset.VPA}");
		Console.WriteLine($"Patrimonio: {asset.Equity}");

		foreach (var dividend in asset.Dividends)
			Console.WriteLine($"{dividend.Key}: {dividend.Value}");


		foreach (var price in asset.Prices.Take(10))
			Console.WriteLine($"{price.Key}: {price.Value}");
	}
	static void printStats(BDRStats asset)
	{
		Console.WriteLine($"DY AVG: {asset.DividendAVG}");
		Console.WriteLine($"Bazin: {asset.BazinPrice}");
		Console.WriteLine($"Graham: {asset.GrahamPrice}");
		Console.WriteLine($"Fast AVG: {asset.FastAvg}");
		Console.WriteLine($"Slow AVG: {asset.SlowAvg}");
		Console.WriteLine($"Down Trend: {asset.DownTrend}");
		Console.WriteLine($"HasAcceptable ROE: {asset.HasAcceptableROE}");
	}
	static void printStats(REITStats asset)
	{
		Console.WriteLine($"BazinPrice: {asset.BazinPrice}");
		Console.WriteLine($"DividendAVG: {asset.DividendAVG}");
		Console.WriteLine($"SlowAvg: {asset.SlowAvg}");
		Console.WriteLine($"FastAvg: {asset.FastAvg}");
		Console.WriteLine($"DYAvgRecent: {asset.DYAvgRecent}");
		Console.WriteLine($"DYAvgConsolidated: {asset.DYAvgConsolidated}");
		Console.WriteLine($"BazinCeilingPriceAboveCurrent: {asset.BazinCeilingPriceAboveCurrent}");
		Console.WriteLine($"CurrentPriceBelowPortfolioAverage: {asset.CurrentPriceBelowPortfolioAverage}");
		Console.WriteLine($"HasEnoughYearsOfIPO: {asset.HasEnoughYearsOfIPO}");
		Console.WriteLine($"DownTrend: {asset.DownTrend}");
		Console.WriteLine($"PVPBellowThreshold: {asset.PVPBellowThreshold}");
		Console.WriteLine($"ManagementFeeBellowThreshold: {asset.ManagementFeeBellowThreshold}");
		Console.WriteLine($"VacancyBellowThreshold: {asset.VacancyBellowThreshold}");
		Console.WriteLine($"AssetValueAboveThreshold: {asset.AssetValueAboveThreshold}");
		Console.WriteLine($"RegionsAboveThreshold: {asset.RegionsAboveThreshold}");
		Console.WriteLine($"PropertyAmountAboveThreshold: {asset.PropertyAmountAboveThreshold}");
		Console.WriteLine($"DailyLiquidityThreshold: {asset.DailyLiquidityAboveThreshold}");
		Console.WriteLine($"DYAboveThresholdRecent: {asset.DYAboveThresholdRecent}");
		Console.WriteLine($"DYAboveThresholdConsolidated: {asset.DYAboveThresholdConsolidated}");
		Console.WriteLine($"RealROIAboveThresholdRecent: {asset.RealROIAboveThresholdNear}");
		Console.WriteLine($"RealROIAboveThresholdConsolidated: {asset.RealROIAboveThresholdLong}");
		Console.WriteLine($"NominalROIAboveThresholdRecent: {asset.NominalROIAboveThresholdNear}");
		Console.WriteLine($"NominalROIAboveThresholdConsolidated: {asset.NominalROIAboveThresholdLong}");
		Console.WriteLine($"DividendAVG: {asset.DividendAVG}");
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
		Task.WaitAll(GetService<IPriceUpdater>().UpdateAll(a => { }));
		ListNullPriceTickers();
		Console.WriteLine("Atualizado");
	}
	static void UpdateMissingPrices()
	{
		Task.WaitAll(GetService<IPriceUpdater>().UpdateMissing(a => { }));
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
			.AddScoped(sp => new DataJSONFilesConfig { BasePath = "C:\\Dev\\StockBrain\\DEV" })
					.AddScoped(sp => new StockEvaluationConfig
					{
						BazinExpectedReturn = 0.06,
						FastAvgSize = 13,
						SlowAvgSize = 90,
						AgeThreshold = 15,
						GrahamConstant = 22.5,
						BazinYearAmount = 5,
						DailyLiquidityThreshold = 2000000,
						ProfitableTimeInQuarters = 20,
						ROEThreshold = 0.1,
						IPOTimeThreshold = 10,
						DividendYieldThreshold = 0.05,
						DividendYieldTimeInYears = 5,
						ProfitGrowthTimeInYears = 5,
						RevenueGrowthTimeInYears = 5,
						NearROIInYears = 2,
						MiddleROIInYears = 5,
						LongROIInYears = 10,
						NominalROIThresholdNear = 0.15,
						NominalROIThresholdMiddle = 0.3,
						NominalROIThresholdLong = 0.8,
						RealROIThresholdNear = 0.05,
						RealROIThresholdMiddle = 0.15,
						RealROIThresholdLong = 0.5,
						PLThreshold = 15,
						PVPThreshold = 2
					})
					.AddScoped(sp => new BDREvaluationConfig
					{
						BazinExpectedReturn = 0.06,
						FastAvgSize = 13,
						SlowAvgSize = 90,
						AgeThreshold = 15,
						GrahamConstant = 22.5,
						BazinYearAmount = 5,
						DailyLiquidityThreshold = 2000000,
						ProfitableTimeInQuarters = 20,
						ROEThreshold = 0.1,
						IPOTimeThreshold = 10,
						DividendYieldThreshold = 0.05,
						DividendYieldTimeInYears = 5,
						ProfitGrowthTimeInYears = 5,
						RevenueGrowthTimeInYears = 5,
						NearROIInYears = 2,
						MiddleROIInYears = 5,
						LongROIInYears = 10,
						NominalROIThresholdNear = 0.15,
						NominalROIThresholdMiddle = 0.3,
						NominalROIThresholdLong = 0,
						RealROIThresholdNear = 0.05,
						RealROIThresholdMiddle = 0.15,
						RealROIThresholdLong = 0,
						PLThreshold = 15,
						PVPThreshold = 2
					})
					.AddScoped(sp => new REITEvaluationConfig
					{
						BazinExpectedReturn = 0.007,
						FastAvgSize = 13,
						SlowAvgSize = 90,
						IPOTimeThreshold = 5,
						AssetValueThreshold = 2000000,
						BazinYearAmount = 2,
						DailyLiquidityThreshold = 2000000,
						DividendYieldConsolidatedAmount = 24,
						DividendYieldConsolidatedThreshold = 0.006,
						DividendYieldRecentAmount = 12,
						DividendYieldRecentThreshold = 0.006,
						ManagementFeeThreshold = 0.01,
						NearROIInYears = 2,
						MiddleROIInYears = 5,
						LongROIInYears = 10,
						NominalROIThresholdNear = 0.15,
						NominalROIThresholdMiddle = 0.3,
						NominalROIThresholdLong = 0,
						RealROIThresholdNear = 0.02,
						RealROIThresholdMiddle = 0.1,
						RealROIThresholdLong = 0.0,
						VacancyRateThreshold = 0.1,
						PropertyThreshold = 15,
						RegionsThreshold = 4,
						PVPThreshold = 1

					})
					.AddScoped<IAccounts, Accounts>()
					.AddScoped<IAssets, Assets>()
					.AddScoped<ISectors, Sectors>()
					.AddScoped<ISegments, Segments>()
					.AddScoped<IBrokers, Brokers>()
					.AddScoped<IBondIssuers, BondIssuers>()
					.AddScoped<IBonds, Bonds>()
					.AddScoped<IStockInfos, StockInfos>()
					.AddScoped<IBDRInfos, BDRInfos>()
					.AddScoped<IREITInfos, REITInfos>()
					.AddScoped<IPortfolioAssets, PortfolioAssets>()
					.AddScoped<IPortfolioAssetMovements, PortfolioAssetMovements>()
					.AddScoped<IPortfolioAssetBrokers, PortfolioAssetBrokers>()
					.AddScoped<IPortfolios, Portfolios>()
					.AddScoped<IPriceGetter, BrAPIMarketPriceGetter>()
					.AddScoped<IPriceUpdater, PriceUpdater>()
					.AddScoped<IDecisionFactors, DecisionFactors>()
					.AddScoped<IAssetInfoUpdater, InvestidorDezAssetInfoUpdater>()
					.AddScoped<IAssetInfos, AssetInfos>()
					.AddScoped<IDecisionFactorAnswerSetter, DecisionFactorAnswerSetter>()
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


