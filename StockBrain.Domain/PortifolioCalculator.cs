using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Extensions;
using StockBrain.Utils;

namespace StockBrain.Domain;

public class PortifolioCalculator : IPortifolioCalculator
{
	public Portfolio Calc(BaseEntity portifolio, Dictionary<AssetType, double> targets, string name, bool main, IEnumerable<PortfolioAsset> assets, IEnumerable<Bond> bonds)
	{
		var variableTotal = assets.Sum(a => a.CurrentValue).ToPrecision(2);
		var fixedTotal = bonds.Sum(a => a.Value).ToPrecision(2);
		var total = (variableTotal + fixedTotal).ToPrecision(2);
		var types = GetTypeDetails(total, targets, assets, bonds);

		return new Portfolio
		{
			ID = portifolio.ID,
			GUID = portifolio.GUID,
			Name = name,
			Main = main,
			Total = total,
			Bonds = bonds,
			Types = types,
			Categories = GetCategoryDetails(total, types),
			Assets = GetAssets(assets, types, total)
		};
	}

	IEnumerable<PortfolioAssetDetail> GetAssets(IEnumerable<PortfolioAsset> assets, IDictionary<AssetType, PortfolioAssetGroup> types, double total)
	{
		return types.SelectMany(t => GetAssets(assets.Where(a => a.Asset.Type == t.Key), t.Value.Target, t.Value.Current, total));
	}
	IEnumerable<PortfolioAssetDetail> GetAssets(IEnumerable<PortfolioAsset> assets, PercentageValue target, PercentageValue actual, double total)
	{
		var portfolioAssets = new List<PortfolioAssetDetail>();
		var typeTotal = assets.Sum(a => a.CurrentValue);
		var typeTarget = total * target.Proportion;
		var totalScore = assets.Sum(a => a.Points());

		foreach (var asset in assets)
		{
			var typePercentage = (totalScore > 0 ? (asset.Points() / (double)totalScore) : 0).ToPrecision(4);
			var globalPercentage = (typePercentage * target.Proportion).ToPrecision(4);
			var assetTarget = new PercentageValue(globalPercentage, total, 2);

			portfolioAssets.Add(new PortfolioAssetDetail
			{
				Asset = asset,
				InvestedOnTotal = new PercentageValue(asset.CurrentValue, total),
				InvestedType = new PercentageValue(asset.CurrentValue, typeTotal), 
				Target = assetTarget,
				DeltaTarget = new DeltaValue(assetTarget.Value, asset.CurrentValue),

			});
		}


		return portfolioAssets;
	}
	IDictionary<AssetCategory, PortfolioAssetGroup> GetCategoryDetails(double total, IDictionary<AssetType, PortfolioAssetGroup> types)
	{

		var categories = new Dictionary<AssetCategory, PortfolioAssetGroup>();
		foreach (var category in types.GroupBy(t => t.Key.Category()))
		{
			var target = category.Sum(c => c.Value.Target.Proportion);
			var current = category.Sum(c => c.Value.Current.Value);
			var invested = category.Sum(c => c.Value.DeltaInvested.Initial);
			categories.Add(category.Key, new PortfolioAssetGroup(total, target, category.Key.ToString(), current, invested));
		}
		return categories;
	}
	IDictionary<AssetType, PortfolioAssetGroup> GetTypeDetails(double total, Dictionary<AssetType, double> targets, IEnumerable<PortfolioAsset> assets, IEnumerable<Bond> bonds)
	{

		var portifolioAssetTypes = new Dictionary<AssetType, PortfolioAssetGroup>();
		foreach (var target in targets)
		{
			var type = target.Key.Category() == AssetCategory.Fix ? 
							GetTypeDetails(total, target.Value, target.Key, bonds.Where(a => a.AssetType == target.Key && !a.Expired)) 
						  : GetTypeDetails(total, target.Value, target.Key, assets.Where(a => a.Asset.Type == target.Key));

			portifolioAssetTypes.Add(target.Key, type);
		}
		return portifolioAssetTypes;
	}
	PortfolioAssetGroup GetTypeDetails(double total, double target, AssetType type, IEnumerable<PortfolioAsset> assets)
	{
		var currentValue = assets.Sum(a => a.CurrentValue);
		var investedValue = assets.Sum(a => a.InvestedValue);
		return new PortfolioAssetGroup(total, target, type.ToString(), currentValue, investedValue);
	}
	PortfolioAssetGroup GetTypeDetails(double total, double target, AssetType type, IEnumerable<Bond> bonds)
	{
		var value = bonds.Sum(a => a.Value);
		return new PortfolioAssetGroup(total, target, type.ToString(), value, value);
	}
}
