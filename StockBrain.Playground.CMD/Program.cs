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

namespace StockBrain.Playground.CMD;

internal class Program
{
	static ServiceProvider ServiceProvider { get; set; }
	static long PortifolioID { get; set; } = 1;
	static async Task Main(string[] args)
	{
		BuildServices();
		CreateAccount("Pedro", "PC");
		//NewFormatETL();
		//CreateAsset("WEGE3",
		//	"WEG S.A.",
		//	"Empresa multinacional brasileira que atua na fabricação e comercialização de equipamentos eletroeletrônicos, com foco em motores, automação e geração de energia.",
		//	new DateOnly(1961, 9, 16),
		//	new DateOnly(1971, 6, 12),
		//	"Exposição a ciclos econômicos globais e variações cambiais. Pressões competitivas no setor industrial e riscos regulatórios em mercados externos.",
		//	"Liderança global em motores elétricos e automação. Forte presença internacional, inovação contínua e crescimento sustentável.",
		//	"Bens Industriais",
		//	"Motores, Compressores e Outros",
		//	AssetType.Acoes
		//);
		Console.ReadKey();
	}
	static void CreateAsset(
		string ticker,
		string name,
		string description,
		DateOnly fundation,
		DateOnly ipo,
		string negativeNotes,
		string positiveNotes,
		string sectorName,
		string segmentName,
		AssetType type) {
		GetService<IAssetCreator>().CreateAndAddToPortfolio(ticker, name, description, fundation, ipo, negativeNotes, positiveNotes, sectorName, segmentName, type);
		Console.WriteLine("Ativo Criado");
	}
	static DBClient GetFirebaseClient() => GetService<DBClient>();

	static void CreateAccount(string accountName, string portifolioName)
	{
		var client = GetFirebaseClient();
		var context = GetService<Context>();
		var portfolioGUID = Guid.NewGuid().ToString();
		var account = new Account { Name = accountName, MainVarBroker = "524d107a-a4c1-4afc-a610-a6c837baaf1f", GUID = Guid.NewGuid().ToString(), MainPortfolio = portfolioGUID };


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
		Console.WriteLine($"{accountName} criado {account.GUID}");
	}
	static void Save<TEntity>(DBClient client, string path, TEntity entity) where TEntity : BaseEntity => client.GetContext<TEntity>(path).Save(entity);
	static async Task CreateInfos(params string[] tickers) => await GetService<IAssetInfoUpdater>().UpdateAll(null, tickers);
	private static T GetService<T>() => ServiceProvider.GetService<T>();
	private static void BuildServices()
	{
		ServiceProvider = new ServiceCollection()
			.AddScoped(sp => new BrAPIConfig { ApiKey = "2MVc6qfPniXFuAaDyMnFDf" })
			.AddScoped(sp => new Context { Account = new Account { GUID = "35557e43-e295-4321-bc63-652b1c7870bc", MainVarBroker = "524d107a-a4c1-4afc-a610-a6c837baaf1f", Name = "Glauber", MainPortfolio = "123e4567-e89b-12d3-a456-426614174000" } })
			.AddSingleton(sp => new DataBaseConfig("https://stock-brain-bd238-default-rtdb.firebaseio.com/", "ku6qpXHMJ4KfpOecG8ZiBtO8xeuW73owmQMdDhwv"))
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
					.AddScoped<IAssetDataCreator, AssetDataCreator>()
					.AddScoped<IAssetCreator, AssetCreator>()
					.AddScoped<IDecisionFactorAnswerSetter, DecisionFactorAnswerSetter>()
			.BuildServiceProvider();
	}
}


