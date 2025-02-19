using Radzen;
using StockBrain.Domain;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase;
using StockBrain.Infra.Repositories.Firebase.FirebaseServices;
using StockBrain.InvestidorDez;
using StockBrain.Services;
using StockBrain.Services.Abstrations;

using StockBrain.WebApp.Components;
using StockBrain.WebApp.Services;

namespace StockBrain.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder
				.Services
				.AddRazorComponents()
				.AddInteractiveServerComponents();

			builder
				.Services
				.AddScoped<Authenticator>()
				.AddCascadingAuthenticationState()
				.AddAuthentication(Authenticator.Scheme)
				.AddCookie(Authenticator.Scheme, op =>
				{
					op.LoginPath = "/login";

					op.Cookie.Name = Authenticator.Scheme;
					op.Cookie.HttpOnly = true;
					op.Cookie.SecurePolicy = CookieSecurePolicy.Always;
					op.Cookie.SameSite = SameSiteMode.Strict;

					op.ExpireTimeSpan = TimeSpan.FromDays(10);
					op.SlidingExpiration = true;

				});

			builder.Services.AddHttpContextAccessor();

			builder.Services.AddRadzenComponents();



			AddServices(builder.Services, builder.Configuration["DataPath"], builder.Configuration["BrAPIKey"]);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCookiePolicy();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();


			app.Run();
		}

		static void AddServices(IServiceCollection services, string dataPath, string brAPIKey)
		{
			services
					.AddScoped(sp => new BrAPIConfig { ApiKey = brAPIKey })
					.AddScoped(sp =>
					{
						var context = new Context();
						if (sp.GetService<Authenticator>().IsAuthenticated())
							context.Account = sp.GetService<Authenticator>().GetAccount();
						return context;
					})
					.AddSingleton(sp => new FirebaseConfigModel
					{
						Secret = "gO5jwO3ysMdTSkzUQmnPWiCnpkIAHBv4F8KYb48p",
						BasePath = "https://stock-brain-qa-default-rtdb.firebaseio.com"

					})
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
					.AddScoped<IPortfolioAssets, PortfolioAssets>()
					.AddScoped<IPortfolioAssetMovements, PortfolioAssetMovements>()
					.AddScoped<IInvestmentRecommender, InvestmentRecommender>()
					.AddScoped<IPortifolioCalculator, PortifolioCalculator>()
					.AddScoped<IPortfolios, Portfolios>()
					.AddScoped<IPriceGetter, BrAPIMarketPriceGetter>()
					.AddScoped<IPriceUpdater, PriceUpdater>()
					.AddScoped<IAssetMovements, AssetMovements>()
					.AddScoped<IBondMovements, BondMovements>()
					.AddScoped<IPortfolioAssetBrokers, PortfolioAssetBrokers>()
					.AddScoped<IPortfolioAssetManager, PortfolioAssetManager>()
					.AddScoped<IInvestmentRecommenderConfigCalculator, InvestmentRecommenderConfigCalculator>()
					.AddScoped<IStockInfos, StockInfos>()
					.AddScoped<IBDRInfos, BDRInfos>()
					.AddScoped<IREITInfos, REITInfos>()
					.AddScoped<IDecisionFactors, DecisionFactors>()
					.AddScoped<IAssetInfoUpdater, InvestidorDezAssetInfoUpdater>()
					.AddScoped<IAssetInfos, AssetInfos>()
					.AddScoped<IDecisionFactorAnswerSetter, DecisionFactorAnswerSetter>()
					.AddScoped<Dialogs>()
					.BuildServiceProvider();
		}
	}
}
