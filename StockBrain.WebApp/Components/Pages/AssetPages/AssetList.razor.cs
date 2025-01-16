using Microsoft.AspNetCore.Components;
using Radzen;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.WebApp.Services;

namespace StockBrain.WebApp.Components.Pages.AssetPages
{
	public partial class AssetList
	{
		[Inject]
		Dialogs Dialogs { get; set; }

		[Inject]
		IAssets Repository { get; set; }
		IEnumerable<Asset> Assets { get; set; } = new List<Asset>();
		Asset Asset { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Assets = Repository.All();
		}

	}
}
