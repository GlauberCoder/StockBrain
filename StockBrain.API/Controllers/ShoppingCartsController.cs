using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Models;
using StockBrain.API.Models.Requests;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase;
using StockBrain.Services.Abstrations;

namespace StockBrain.API.Controllers;

/// <summary>
/// API controller that provides endpoints for retrieving fixed income asset metadata, such as bond types and indexes.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class ShoppingCartsController : Controller
{

	IAssetMovements Assets { get; }
	IBondMovements Bonds { get; }
	IPortfolioAssetManager PortfolioAssetManager { get; }

	public ShoppingCartsController(IAssetMovements assets, IBondMovements bonds, IPortfolioAssetManager portfolioAssetManager)
	{
		Assets = assets;
		Bonds = bonds;
		PortfolioAssetManager = portfolioAssetManager;
	}

	[HttpGet]
	public ShoppingCart Get() => new ShoppingCart(Assets.All(), Bonds.All());



	[HttpPost("Assets/Add")]
	public ShoppingCart AddAssets(IEnumerable<AssetMovementDTO> movements)
	{
		foreach (var movement in movements)
			Assets.Add(movement);
		return Get();
	}
	[HttpPost("Assets/Save")]
	public ShoppingCart SaveAsset(IEnumerable<ShoppingCartAssetSaveRequest> assets)
	{
		foreach (var asset in assets)
		{
			var assetMovement = Assets.ByID(asset.MovementGUID);
			assetMovement.Quantity = asset.Quantity;
			assetMovement.Investment = asset.Investment;
			Assets.Save(assetMovement);
		}
		return Get();
	}
	[HttpPost("Bonds/Add")]
	public ShoppingCart AddBonds(IEnumerable<BondMovementDTO> movements)
	{
		foreach (var movement in movements)
			Bonds.Add(movement);
		return Get();
	}
	[HttpDelete("Assets/Delete")]
	public ShoppingCart DeleteAssets(IEnumerable<string> uuids)
	{
		Assets.Delete(uuids);
		return Get();
	}
	[HttpDelete("Bonds/Delete")]
	public ShoppingCart DeleteBonds(IEnumerable<string> uuids)
	{
		Bonds.Delete(uuids);
		return Get();
	}
	[HttpDelete("Assets/Delete/All")]
	public ShoppingCart ClearAssets()
	{
		Assets.Clear();
		return Get();
	}
	[HttpDelete("Bonds/Delete/All")]
	public ShoppingCart ClearBonds()
	{
		Bonds.Clear();
		return Get();
	}
	[HttpPost("Assets/Define/Broker/{brokerUUID}")]
	public ShoppingCart AssetsDefineBroker(string brokerUUID, [FromBody] IEnumerable<string> assetsUUIDs)
	{
		Assets.DefineBroker(brokerUUID, assetsUUIDs);
		return Get();
	}
	[HttpPost("Bonds/Define/Broker/{brokerUUID}")]
	public ShoppingCart BondsDefineBroker(string brokerUUID, [FromBody] IEnumerable<string> bondsUUIDs)
	{
		Bonds.DefineBroker(brokerUUID, bondsUUIDs);
		return Get();
	}
	[HttpPost("Execute")]
	public ShoppingCart Execute(ShoppingCartExecutionRequest request)
	{
		PortfolioAssetManager.ConfirmMovements(request.PortfolioUUIDs, request.AssetUUIDs, request.BondUUIDs).Wait();
		return Get();
	}
}
