using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Models.VMs;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SummaryController : Controller
{
	IPortfolios Portfolios { get; set; }
	public SummaryController(IPortfolios portfolios)
	{
		Portfolios = portfolios;
	}


	[HttpGet("{portfolioUUID}")]
	public SummaryDashboardVM Get(string portfolioUUID)
	{
		return new SummaryDashboardVM(Portfolios.ByID(portfolioUUID));
	}
}
