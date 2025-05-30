using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

/// <summary>
/// API controller that provides endpoints for retrieving portfolio summaries and asset details.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class PortfolioSummariesController : Controller
{
	/// <summary>
	/// Gets or sets the portfolios repository.
	/// </summary>
	IPortfolios Portfolios { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="PortfolioSummariesController"/> class.
	/// </summary>
	/// <param name="portfolios">The portfolios repository instance.</param>
	public PortfolioSummariesController(IPortfolios portfolios) => Portfolios = portfolios;

	/// <summary>
	/// Retrieves a summary dashboard for the specified portfolio.
	/// </summary>
	/// <param name="portfolioUUID">The unique identifier of the portfolio.</param>
	/// <returns>A <see cref="SummaryDashboardVM"/> containing the portfolio summary.</returns>
	[HttpGet("{portfolioUUID}")]
	public Portfolio Get(string portfolioUUID) =>
		Portfolios.ByID(portfolioUUID);

}
