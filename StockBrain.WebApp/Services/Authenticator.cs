using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using System.Security.Claims;

namespace StockBrain.WebApp.Services;

public class Authenticator
{
	IAccounts Accounts { get; }
	IHttpContextAccessor HttpContextAccessor { get; }
	public const string Scheme = "StockBrainAuth";

	public Authenticator(IAccounts accounts, IHttpContextAccessor httpContextAccessor)
	{
		Accounts = accounts;
		HttpContextAccessor = httpContextAccessor;
	}

	public async Task<Account> Authenticate(string accountUUID) 
	{
		var account = Accounts.Get(accountUUID);
		if (account != null)
		{
			var httpContext = HttpContextAccessor.HttpContext;
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, account.Name),
				new Claim(ClaimTypes.NameIdentifier, accountUUID)
			};
			var identity = new ClaimsIdentity(claims, Scheme);
			var principal = new ClaimsPrincipal(identity);
			await httpContext.SignInAsync(Scheme, principal);

		}
		return account;
	}
	public Task SignOut() => HttpContextAccessor.HttpContext.SignOutAsync();
	public bool IsAuthenticated() => HttpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated??false;
	public string GetName() => HttpContextAccessor.HttpContext.User.Identity.Name;
	public string GetUUID() => HttpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
	public Account GetAccount() => IsAuthenticated() ? Accounts.Get(GetUUID()) : null;
}
