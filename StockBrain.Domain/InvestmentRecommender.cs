using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Extensions;
using StockBrain.Domain.Models.Model;
using StockBrain.Utils;

namespace StockBrain.Domain;

public class InvestmentRecommender : IInvestmentRecommender
{
	Context Context { get; }

	public InvestmentRecommender(Context context)
	{
		Context = context;
	}
	public InvestmentRecommendation Recommend(Portfolio portfolio, double investment, IDictionary<AssetType, InvestmentRecommendationTypeConfig> config)
	{
		var newTotal = (portfolio.Total + investment).ToPrecision(2);
		var types = GetTypes(portfolio, investment, newTotal, config);
		return new InvestmentRecommendation(types, investment, newTotal, Context.Today);
	}

	IEnumerable<InvestmentGroupAsset> GetAssets(AssetType type, Portfolio portfolio, double newTotal)
	{
		return portfolio.Assets.Where(a => a.Asset.Asset.Type == type).Select(a => GetType(a, newTotal)).ToList();
	}
	InvestmentGroupAsset GetType(PortfolioAssetDetail asset, double newTotal)
	{
		var target = new PercentageValue(asset.Target.Proportion, newTotal, 2);
		return new InvestmentGroupAsset
		{
			Name = asset.Asset.Asset.Ticker,
			Asset = asset,
			Target = target,
			Current = new PercentageValue(asset.Asset.CurrentValue, newTotal),
			DeltaTarget = new DeltaValue(asset.Asset.CurrentValue, target.Value)
		};

	}
	IEnumerable<InvestmentGroupType> GetTypes(Portfolio portfolio, double investment, double newTotal, IDictionary<AssetType, InvestmentRecommendationTypeConfig> config)
	{
		var results = portfolio.Types.Select(t => GetType(portfolio, t.Value, t.Key, newTotal, config[t.Key].Amount, config)).ToList();
		RecommendValue(results, null, investment, newTotal);
		foreach (var type in results.Where(t => t.Type.Category() == AssetCategory.Var))
			type.Assets = RecommendValue(type.Assets, type.MaxRecommendations, type.Investment.Value, newTotal);

		return results;
	}
	InvestmentGroupType GetType(Portfolio portfolio, PortfolioAssetGroup group, AssetType type, double newTotal, int? max, IDictionary<AssetType, InvestmentRecommendationTypeConfig> config)
	{
		var target = new PercentageValue(config[type].Target, newTotal, 2);
		return new InvestmentGroupType
		{
			Name = group.Name,
			Type = type,
			Target = target,
			Current = group.Current,
			MaxRecommendations = max,
			DeltaTarget = new DeltaValue(group.Current.Value, target.Value),
			Assets = GetAssets(type, portfolio, newTotal)
		};

	}
	IEnumerable<T> RecommendValue<T>(IEnumerable<T> groups, int? max, double investment, double newTotal)
		where T : InvestmentGroup
	{
		var itens = max.HasValue ? groups.OrderByDescending(g => g.DeltaTarget.Difference).Take(max.Value) : groups;
		var totalDeltaTarget = itens.Sum(g => Math.Max(g.DeltaTarget.Difference, 0));

		foreach (var item in itens)
			item.SetInvestment(Math.Max(item.DeltaTarget.Difference, 0) / totalDeltaTarget, investment, newTotal);

		return itens;

	}
}
