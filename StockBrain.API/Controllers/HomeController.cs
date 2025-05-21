using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class HomeController : Controller
{
	IPortfolios Portifolios { get; set; }
	public HomeController(IPortfolios portifolios)
	{
		Portifolios = portifolios;
	}


	[HttpGet]
	public IEnumerable<Portfolio> Get()
	{
		return Portifolios.All().OrderBy(p => p.Name);
	}
}
