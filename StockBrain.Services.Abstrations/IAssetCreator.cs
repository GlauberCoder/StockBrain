using StockBrain.Domain.Models.Enums;

namespace StockBrain.Services.Abstrations;

public interface IAssetCreator
{
	void CreateAndAddToPortfolio(
		string ticker,
		string name,
		string description,
		DateOnly fundation,
		DateOnly ipo,
		string negativeNotes,
		string positiveNotes,
		string sectorName,
		string segmentName,
		AssetType type
	);
}
