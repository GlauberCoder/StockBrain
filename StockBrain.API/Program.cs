using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockBrain.API.Models;
using StockBrain.API.Services;
using StockBrain.Domain;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase;
using StockBrain.Infra.Repositories.Firebase.Services;
using StockBrain.InvestidorDez;
using StockBrain.Services;
using StockBrain.Services.Abstrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var environment = builder.Configuration["Environment"];
var brAPIKey = builder.Configuration["BrAPIKey"];
var firebaseBasePath = builder.Configuration[$"EnvironmentsConfigs:{environment}:FireBaseBasePath"];
var firebaseAuthSecret = builder.Configuration[$"EnvironmentsConfigs:{environment}:FireBaseAuthSecret"];
var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = false,
			RequireSignedTokens = false
		};
	});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder
	.Services
			.AddScoped<JwtConfig>( sp => jwtConfig)
			.AddScoped<Authenticator>()
			.AddScoped(sp => new BrAPIConfig { ApiKey = brAPIKey })
			.AddScoped(sp =>
			{
				var account = sp.GetService<Authenticator>().GetAccount();
				var context = new Context();
				context.Name = environment;
				if (account != null)
					context.Account = account;

				return context;
			})
			.AddSingleton(sp => new DataBaseConfig(firebaseBasePath, firebaseAuthSecret))
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
			.AddScoped<IAssetMovementFromCSVUpdater, AssetMovementFromCSVUpdater>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
