using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Services;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetsController : Controller
{
	/// <summary>
	/// Gets or sets the portfolios repository.
	/// </summary>
	IPortfolios Portfolios { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="PortfolioSummariesController"/> class.
	/// </summary>
	/// <param name="portfolios">The portfolios repository instance.</param>
	public AssetsController(IPortfolios portfolios) => Portfolios = portfolios;

	/// <summary>
	/// Retrieves the details of a specific asset in the portfolio by its ticker symbol.
	/// </summary>
	/// <param name="portfolioUUID">The unique identifier of the portfolio.</param>
	/// <param name="ticker">The ticker symbol of the asset.</param>
	/// <returns>A <see cref="PortfolioAssetDetail"/> with the asset details, or null if not found.</returns>
	[HttpGet("Ticker/{ticker}/Portfolio/{portfolioUUID}")]
	public PortfolioAssetDetail AssetByTicker(string portfolioUUID, string ticker) =>
		Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker);

}
