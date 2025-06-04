using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;


[ApiController]
[Route("[controller]")]
public class PortfoliosController : Controller
{

	public PortfoliosController(IPortfolios portfolios)
	{
		Portfolios = portfolios;
	}

	IPortfolios Portfolios { get; }

	[HttpGet("List")]
	public IEnumerable<dynamic> List() => Portfolios.All().Select(p => new EntityReference(p.GUID,p.Name));
}
