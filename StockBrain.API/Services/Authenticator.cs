using Microsoft.IdentityModel.Tokens;
using StockBrain.API.Models;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockBrain.API.Services;

public class Authenticator
{
	IAccounts Accounts { get; }
	IHttpContextAccessor HttpContext { get; }
	JwtConfig Config { get; }

	public Authenticator(IAccounts accounts, IHttpContextAccessor httpContext, JwtConfig config)
	{
		Accounts = accounts;
		HttpContext = httpContext;
		Config = config;
	}


	public async Task<string> Authenticate(string accountUUID)
	{
		if (string.IsNullOrWhiteSpace(accountUUID))
			return null;

		var account = Accounts.ByID(accountUUID);
		if (account == null)
			return null;

		var token = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, account.Name),
				new Claim(ClaimTypes.NameIdentifier, accountUUID)
			})
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var jwtToken = tokenHandler.CreateToken(token);

		return tokenHandler.WriteToken(jwtToken);
	}
	public Account GetAccount()
	{
		var httpContext = HttpContext.HttpContext;
		if (!(httpContext?.User?.Identity?.IsAuthenticated ?? false))
			return null;

		var accountUUID = httpContext.User
			.Claims
			.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
			?.Value;

		return Accounts.ByID(accountUUID);
	}
}
