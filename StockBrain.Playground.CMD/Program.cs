using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.DependencyInjection;
using StockBrain.Domain;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.DTOs;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase;
using StockBrain.Infra.Repositories.Firebase.Services;
using StockBrain.InvestidorDez;
using StockBrain.Services;
using StockBrain.Services.Abstrations;
using StockBrain.Utils;
using System.IO;

namespace StockBrain.Playground.CMD;

internal class Program
{
	static ServiceProvider ServiceProvider { get; set; }
	static long PortifolioID { get; set; } = 1;
	static async Task Main(string[] args)
	{
		BuildServices();
		//CreateAccount("Higor", "Fisher");
		NewFormatETL();
		Console.ReadKey();
	}

	static void NewFormatETL()
	{
		var source = new FirebaseClient("https://stock-brain-bd238-default-rtdb.firebaseio.com", new FirebaseOptions
		{
			AuthTokenAsyncFactory = () => Task.FromResult("gO5jwO3ysMdTSkzUQmnPWiCnpkIAHBv4F8KYb48p")
		});
		var destiny = new FirebaseClient("https://stock-brain-qa-default-rtdb.firebaseio.com", new FirebaseOptions
		{
			AuthTokenAsyncFactory = () => Task.FromResult("gO5jwO3ysMdTSkzUQmnPWiCnpkIAHBv4F8KYb48p")
		});

		var translations = new Dictionary<string, Dictionary<long, string>>();

		destiny.Child("decisionFactors").PutAsync(source.Child("decisionFactors").OnceSingleAsync<Dictionary<AssetType, IEnumerable<string>>>().Result);
		Copy(source, destiny, "accounts", ToAccount, translations);
		Copy(source, destiny, "bondIssuers", ToBondIssuer, translations);
		Copy(source, destiny, "brokers", ToBroker, translations);
		Copy(source, destiny, "segments", ToSegment, translations);
		Copy(source, destiny, "sectors", ToSector, translations);
		Copy(source, destiny, "assets", ToAsset, translations);

		var bonds = source
			.Child("bonds")
			.OnceAsync<dynamic>()
			.Result
			.GroupBy(v => (long)v.Object.PortifolioID)
			.ToDictionary(v => v.Key, v => v.Select(b => (BondDTO)ToBond(b.Object, translations)));

		var assets = source
			.Child("portfolioAssets")
			.OnceAsync<dynamic>()
			.Result
			.GroupBy(v => (long)v.Object.PortifolioID)
			.ToDictionary(v => v.Key, v => v.Select(b => (PortfolioAssetDTO)ToPortfolioAsset(b.Object)));

		var portfolios = source
			.Child("portfolios")
			.OnceAsync<dynamic>()
			.Result
			.ToDictionary(v => v.Key, v => ToPortfolio(v.Object, bonds, assets));

		destiny.Child("users/35557e43-e295-4321-bc63-652b1c7870bc/portfolios").PutAsync(portfolios);

		Console.ReadKey();



	}

	static void Copy<TEntity>(FirebaseClient source, FirebaseClient destiny, string path, Func<dynamic, Dictionary<string, Dictionary<long, string>>, TEntity> transformer, Dictionary<string, Dictionary<long, string>> translations)
	{
		var results = source.Child(path).OnceAsync<dynamic>().Result;
		var entities = results.ToDictionary(d => d.Key, d => transformer(d.Object, translations));
		destiny.Child(path).PutAsync(entities);
		translations.Add(path, results.ToDictionary(r => (long)r.Object.ID, r => r.Key));
	}
	static AssetDTO ToAsset(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new AssetDTO 
		{ 
			GUID = value.GUID, 
			Name = value.Name, 
			Description = value.Description,
			Foundation = value.Foundation,
			IPO = value.IPO,
			LastPriceUpdate = value.LastPriceUpdate,
			LastReview = value.LastReview,
			MarketPrice = value.MarketPrice,
			NegativeNotes = value.NegativeNotes,
			PositiveNotes = value.PositiveNotes,
			Risk = value.Risk,
			SectorGUID = translations["sectors"][(long)value.SectorID],
			SegmentGUID = translations["segments"][(long)value.SegmentID],
			Ticker = value.Ticker,
			Type = value.Type
		};
	}
	static PortfolioAssetDTO ToPortfolioAsset(dynamic value)
	{
		return new PortfolioAssetDTO
		{
			GUID = value.GUID,
			FirstAquisition = value.FirstAquisition,
			LastAquisition = value.LastAquisition,
			Quantity = value.Quantity,
			Risk = value.Risk,
			Ticker = value.Ticker,
			Value = value.Value,
			Brokers = new Dictionary<string, PortfolioAssetBrokerDTO> 
			{
				{ "524d107a-a4c1-4afc-a610-a6c837baaf1f", new PortfolioAssetBrokerDTO { Quantity = value.Quantity, Ticker = value.Ticker, BrokerGUID = "524d107a-a4c1-4afc-a610-a6c837baaf1f", GUID = "524d107a-a4c1-4afc-a610-a6c837baaf1f" } }
			}
		};
	}
	static BondDTO ToBond(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new BondDTO { 
			GUID = value.GUID, 
			BrokerGUID = translations["brokers"][(long)value.BrokerID],
			Date = value.Date,
			Expiration = value.Expiration,
			Index = value.Index,
			IssuerGUID = translations["bondIssuers"][(long)value.IssuerID],
			Redeem = value.Redeem,
			Tax = value.Tax,
			Type = value.Type,
			Value = value.Value			
		};
	}
	static PortfolioDTO ToPortfolio(dynamic value, IDictionary<long, IEnumerable<BondDTO>> bonds, IDictionary<long, IEnumerable<PortfolioAssetDTO>> assets
		) 
	{
		return new PortfolioDTO { 
			Name = value.Name,
			GUID = value.GUID,
			Targets = new Dictionary<AssetType, double> {
				{ AssetType.Acoes, 0.3 },
				{ AssetType.BDR, 0.25 },
				{ AssetType.FII, 0.2 },
				{ AssetType.Gov, 0.1 },
				{ AssetType.Priv, 0.15 },
			},
			Assets = assets[(long)value.ID].ToDictionary(a => a.GUID, a => a),
			Bonds = bonds[(long)value.ID].ToDictionary(a => a.GUID, a => a),
		};
	}
	static BondIssuer ToBondIssuer(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new BondIssuer { GUID = value.GUID, Name = value.Name, };
	}
	static Broker ToBroker(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new Broker { GUID = value.GUID, Name = value.Name };
	}
	static Sector ToSector(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new Sector { GUID = value.GUID, Name = value.Name };
	}
	static Segment ToSegment(dynamic value, Dictionary<string, Dictionary<long, string>> translations)
	{
		return new Segment { GUID = value.GUID, Name = value.Name };
	}
	static Account ToAccount(dynamic value, Dictionary<string, Dictionary<long, string>> translations) {
		return new Account { GUID = value.GUID, Name = value.Name, MainPortfolio = value.MainPortfolio };
	}
	static DBClient GetFirebaseClient() => GetService<DBClient>();

	static void CreateAccount(string accountName, string portifolioName)
	{
		var client = GetFirebaseClient();
		var context = GetService<Context>();
		var portfolioGUID = Guid.NewGuid().ToString();
		var account = new Account { Name = accountName, GUID = Guid.NewGuid().ToString(), MainPortfolio = portfolioGUID };


		var assets = GetService<IAssets>().All().Where(a => !a.Risk).Select(a => new PortfolioAssetDTO
		{
			GUID = a.Ticker,
			FirstAquisition = context.Today,
			LastAquisition = context.Today,
			Quantity = 0,
			Value = 0,
			Risk = false,
			Ticker = a.Ticker
		});

		var portfolio = new PortfolioDTO
		{
			Name = portifolioName,
			Targets = new Dictionary<AssetType, double> {
				{ AssetType.Acoes, 0.3 },
				{ AssetType.BDR, 0.25 },
				{ AssetType.FII, 0.2 },
				{ AssetType.Gov, 0.1 },
				{ AssetType.Priv, 0.15 },
			},
			Assets = assets.ToDictionary(a => a.GUID, a => a)
		};

		Save(client, "accounts", account);
		Save(client, $"users/{account.GUID}/portfolios", portfolio);

	}
	static void Save<TEntity>(DBClient client, string path, TEntity entity)
		where TEntity : BaseEntity
	{
		client.GetContext<TEntity>(path).Save(entity);
	}
	static async Task CreateInfos(params string[] tickers)
	{
		await GetService<IAssetInfoUpdater>().UpdateAll(null, tickers);
	}
	private static T GetService<T>() => ServiceProvider.GetService<T>();
	private static void BuildServices()
	{
		ServiceProvider = new ServiceCollection()
			.AddScoped(sp => new BrAPIConfig { ApiKey = "2MVc6qfPniXFuAaDyMnFDf" })
			.AddScoped(sp => new Context { Account = new Account { GUID = "35557e43-e295-4321-bc63-652b1c7870bc",  Name = "Glauber", MainPortfolio = "123e4567-e89b-12d3-a456-426614174000" } })
			.AddSingleton(sp => new DataBaseConfig("https://stock-brain-qa-default-rtdb.firebaseio.com", "gO5jwO3ysMdTSkzUQmnPWiCnpkIAHBv4F8KYb48p"))
			.AddScoped<DBClient>()
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
					.AddScoped<IInvestmentRecommender, InvestmentRecommender>()
					.AddScoped<IPortifolioCalculator, PortifolioCalculator>()
					.AddScoped<IPortfolios, Portfolios>()
					.AddScoped<IPriceGetter, BrAPIMarketPriceGetter>()
					.AddScoped<IPriceUpdater, PriceUpdater>()
					.AddScoped<IAssetMovements, AssetMovements>()
					.AddScoped<IBondMovements, BondMovements>()
					.AddScoped<IPortfolioAssetManager, PortfolioAssetManager>()
					.AddScoped<IInvestmentRecommenderConfigCalculator, InvestmentRecommenderConfigCalculator>()
					.AddScoped<IStockInfos, StockInfos>()
					.AddScoped<IBDRInfos, BDRInfos>()
					.AddScoped<IREITInfos, REITInfos>()
					.AddScoped<IDecisionFactors, DecisionFactors>()
					.AddScoped<IAssetInfoUpdater, InvestidorDezAssetInfoUpdater>()
					.AddScoped<IAssetInfos, AssetInfos>()
					.AddScoped<IDecisionFactorAnswerSetter, DecisionFactorAnswerSetter>()
			.BuildServiceProvider();
	}
}


