using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IAssetMovementFromCSVUpdater
{
	IEnumerable<AssetMovement> Update(string text, IEnumerable<AssetMovement> movements);
}
