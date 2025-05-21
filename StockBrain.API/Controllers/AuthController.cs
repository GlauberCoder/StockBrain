using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Services;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
	Authenticator JwtServices { get; }
	public AuthController(Authenticator jwtServices)
	{
		JwtServices = jwtServices;
	}

	[AllowAnonymous]
	[HttpGet("{accountUUID}")]
	public async Task<ActionResult<string>> Login(string accountUUID)
	{
		var result = await JwtServices.Authenticate(accountUUID);
		if(result == null)
			return Unauthorized();
		return Ok(result);
	}
}
