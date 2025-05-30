using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Model;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class InvestmentRecommendationsController : Controller
{
	IPortfolios Portfolios { get; set; }
	IInvestmentRecommenderConfigCalculator ConfigCalculator { get; }
	IInvestmentRecommender Recommender { get; }

	public InvestmentRecommendationsController(IPortfolios portfolios, IInvestmentRecommenderConfigCalculator configCalculator, IInvestmentRecommender recommender)
	{
		Portfolios = portfolios;
		ConfigCalculator = configCalculator;
		Recommender = recommender;
	}
	[HttpGet("Config/Portfolio/{portfolioUUID}")]
	public IDictionary<AssetType, InvestmentRecommendationTypeConfig> Config(string portfolioUUID) =>
		ConfigCalculator.Calc(Portfolios.ByID(portfolioUUID));

	[HttpPost("Portfolio/{portfolioUUID}/Investment/{investment}")]
	public InvestmentRecommendation Get(string portfolioUUID, double investment, [FromBody] IDictionary<AssetType, InvestmentRecommendationTypeConfig> config) =>
		Recommender.Recommend(Portfolios.ByID(portfolioUUID), investment, config);
}
