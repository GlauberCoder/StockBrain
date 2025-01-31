using Radzen;
using StockBrain.Domain;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Infra.IndicatorGetters.Abstractions;
using StockBrain.Infra.IndicatorGetters.Investidor10;
using StockBrain.Infra.PriceGetters.Abstractions;
using StockBrain.Infra.PriceGetters.BrAPI;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.JSONFiles;
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
					.AddScoped(sp => new DataJSONFilesConfig { BasePath = dataPath })
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
					.AddScoped<IDecisionFactorAnswer, Investidor10DecisionFactorAnswer>()
					.AddScoped<Dialogs>()
					.BuildServiceProvider();
		}
	}
}
