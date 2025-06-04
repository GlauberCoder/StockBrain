using StockBrain.Domain.Models;

namespace StockBrain.API.Models;

public class ShoppingCart
{
	public IEnumerable<AssetMovement> Assets { get; }
	public IEnumerable<BondMovement> Bonds { get; }
	public ShoppingCart(IEnumerable<AssetMovement> assets, IEnumerable<BondMovement> bonds)
	{
		Assets = assets;
		Bonds = bonds;
	}

}
