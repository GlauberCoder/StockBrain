using Microsoft.AspNetCore.Components;
using Radzen;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Model;
using StockBrain.WebApp.Components.Pages.AssetPages;
using StockBrain.WebApp.Components.Pages.InvestmentPages;
using StockBrain.WebApp.Components.Pages.PortfolioPages;
using StockBrain.WebApp.Components.Pages.ShoppingCartPages;

namespace StockBrain.WebApp.Services;

public class Dialogs
{

	public Dialogs(DialogService dialogService)
	{
		DialogService = dialogService;
	}

	DialogService DialogService { get; }
	DialogPosition Position { get; } = DialogPosition.Right;
	bool CloseDialogOnOverlayClick { get; } = false;
	bool ShowMask { get; } = false;
	string Width { get; } = "1200px";
	public void Close()
	{
		DialogService.CloseSide();
	}
	public void InvestmentConfig(Portfolio portfolio, IDictionary<AssetType, InvestmentRecommendationTypeConfig> configs, Action<IDictionary<AssetType, InvestmentRecommendationTypeConfig>> onSave)
	{
		Open<InvestmentConfig>("Config", new Dictionary<string, object>() { { "Portfolio", portfolio }, { "Configs", configs }, { "OnSave", onSave } }, "400px");
	}
	public void CartAddVar(Action saveCallBack)
	{
		Open<CartAddVarPage>("Cart", new Dictionary<string, object>() { { "SaveCallBack", saveCallBack } }, "400px");
	}
	public void CartAddFix(Action saveCallBack)
	{
		Open<CartAddFixPage>("Cart", new Dictionary<string, object>() { { "SaveCallBack", saveCallBack } }, "400px");
	}
	public void Asset(Asset asset)
	{
		Open<AssetDetails>(asset.Ticker, new Dictionary<string, object> { { "Asset", asset } });
	}
	public void PortfolioAsset(PortfolioAssetDetail asset)
	{
		Open<PortifolioAssetDetails>(asset.Asset.Asset.Ticker, new Dictionary<string, object> { { "Asset", asset } });
	}
	public void MovementConfirmation(IEnumerable<AssetMovement> assets, IEnumerable<BondMovement> bonds)
	{
		Open<MovementsConfirmationPage>("Deseja confirmar movimentos ?", new Dictionary<string, object> { { "Assets", assets }, { "Bonds", bonds } });
	}
	void Open<T>(string title, Dictionary<string, object> parameters, string width = null)
		where T : ComponentBase
	{
		DialogService
			.OpenSideAsync<T>(
					title,
					parameters,
					options: new SideDialogOptions
					{
						CloseDialogOnOverlayClick = CloseDialogOnOverlayClick,
						Position = Position,
						ShowMask = ShowMask,
						Width = width ?? Width,
					});

	}
}
