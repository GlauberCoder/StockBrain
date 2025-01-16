using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class InvestmentGroupCategory : InvestmentGroup
{
	public AssetCategory Category { get; }
	public IEnumerable<InvestmentGroupType> Types { get; }
	public override int Level => 1;
	public InvestmentGroupCategory(AssetCategory category, IEnumerable<InvestmentGroupType> types, double totalInvestment, double newTotal)
	{
		Category = category;
		Types = types;

		var investment = types.Sum(t => t.Investment.Value);
		var current = types.Sum(t => t.Current.Value);
		var after = types.Sum(t => t.After.Value);
		var target = types.Sum(t => t.Target.Value);

		Name = category.ToString();
		Target = new PercentageValue(target, newTotal);
		Current = new PercentageValue(current, newTotal);
		DeltaTarget = new DeltaValue(current, target);
		After = new PercentageValue(after, newTotal);
		Investment = new PercentageValue(investment, totalInvestment);


	}
}
