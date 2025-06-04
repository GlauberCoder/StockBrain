using Microsoft.AspNetCore.Mvc;
using StockBrain.API.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.API.Controllers;

/// <summary>
/// API controller that provides endpoints for retrieving fixed income asset metadata, such as bond types and indexes.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShoppingCartsController : Controller
{

	IAssetMovements Assets { get; }
	IBondMovements Bonds { get; }
	public ShoppingCartsController(IAssetMovements assets, IBondMovements bonds)
	{
		Assets = assets;
		Bonds = bonds;
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
	public ShoppingCart AssetsDefineBroker(string brokerUUID, [FromBody]IEnumerable<string> assetsUUIDs)
	{
		Assets.DefineBroker(brokerUUID, assetsUUIDs);
		return Get();
	}
	[HttpPost("Bonds/Define/Broker/{brokerUUID}")]
	public ShoppingCart BondsDefineBroker(string brokerUUID, [FromBody]IEnumerable<string> bondsUUIDs)
	{
		Bonds.DefineBroker(brokerUUID, bondsUUIDs);
		return Get();
	}
}
