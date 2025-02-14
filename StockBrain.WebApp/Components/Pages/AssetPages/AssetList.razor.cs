using Microsoft.AspNetCore.Components;
using Radzen;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;
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
		IDictionary<long, bool> PriceUpdated { get; set; }
		bool EvaluationUpdating { get; set; } = false;

		protected override async Task OnInitializedAsync()
		{
			OnLoadAssets(Repository.All());
		}
		void OnLoadAssets(IEnumerable<Asset> assets) 
		{
			Assets = assets;
			PriceUpdated = Assets.ToDictionary(a => a.ID, a => a.LastPriceUpdate >= Context.Today);
		}


	}
}
