using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models.Extensions;

public static class AssetTypeExtensions
{
	public static AssetCategory Category(this AssetType assetType)
	{
		return assetType switch
		{
			AssetType.Gov => AssetCategory.Fix,
			AssetType.Priv => AssetCategory.Fix,
			_ => AssetCategory.Var
		};
	}
}
