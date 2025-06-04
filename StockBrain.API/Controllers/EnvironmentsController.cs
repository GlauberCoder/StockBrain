using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EnvironmentsController : Controller
{
	public EnvironmentsController(Context context)
	{
		Context = context;
	}

	Context Context { get; }

	[HttpGet]
	public Context Index() => Context;
}
