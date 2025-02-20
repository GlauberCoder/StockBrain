using Microsoft.AspNetCore.Components;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Domain.Models;

namespace StockBrain.WebApp.Components.Pages.PortfolioPages;

public partial class PortfolioList
{
	[Inject]
	IPortfolios Repository { get; set; }
	private bool IsLoading { get; set; }
	IEnumerable<Portfolio> Portfolios { get; set; } = new List<Portfolio>();


	protected override async Task OnInitializedAsync()
	{
		IsLoading = true;
		InvokeAsync(StateHasChanged).ContinueWith(r =>
		{
			Portfolios = Repository.FromCurrentAccount().OrderBy(p => p.Name).OrderByDescending(p => p.Main);
			IsLoading = false;
			InvokeAsync(StateHasChanged);
		});
	}

}
