using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Services;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountsController : Controller
{

	public AccountsController(Context context)
	{
		Context = context;
	}

	Context Context { get; }

	[HttpGet]
	public Account Get() => Context.Account;
}

