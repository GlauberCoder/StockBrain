using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BondIssuersController : Controller
{

	public BondIssuersController(IBondIssuers issuers)
	{
		Issuers = issuers;
	}

	IBondIssuers Issuers { get; }

	[HttpGet]
	public IEnumerable<BondIssuer> Get() => Issuers.All();
	[HttpPost("Name/{name}")]
	public BondIssuer Post(string name) => Issuers.Save(new BondIssuer { Name = name });
}
