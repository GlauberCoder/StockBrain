using Microsoft.AspNetCore.Components;
using Radzen;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.WebApp.Components.Pages.PortfolioPages;

public partial class PortfolioList
{
	[Inject]
	IPortfolios Repository { get; set; }
	IEnumerable<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

	protected override async Task OnInitializedAsync()
	{
		Portfolios = Repository.All().OrderBy(p => p.Name).OrderByDescending(p => p.Main);
	}
}
