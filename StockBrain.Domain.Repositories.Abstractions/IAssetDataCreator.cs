using StockBrain.Domain.Models.Enums;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssetDataCreator
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
