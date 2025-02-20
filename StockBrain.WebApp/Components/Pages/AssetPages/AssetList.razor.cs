using Microsoft.AspNetCore.Components;
using Radzen;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;
using StockBrain.WebApp.Components.Common;
using StockBrain.WebApp.Services;

namespace StockBrain.WebApp.Components.Pages.AssetPages
{
	public partial class AssetList
	{
		[Inject]
		Dialogs Dialogs { get; set; }
		[Inject]
		Context Context { get; set; }
		[Inject]
		IAssets Repository { get; set; }
		IEnumerable<Asset> Assets { get; set; } = new List<Asset>();
		Asset Asset { get; set; }
		bool EvaluationUpdating { get; set; } = false;
		private bool IsLoading { get; set; }

		protected override async Task OnInitializedAsync()
		{
			IsLoading = true;
			InvokeAsync(StateHasChanged).ContinueWith(r =>
			{
				Assets = Repository.All();
				IsLoading = false;
				InvokeAsync(StateHasChanged);
			});
		}


	}
}
