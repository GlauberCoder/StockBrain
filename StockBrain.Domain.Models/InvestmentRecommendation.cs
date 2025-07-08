using StockBrain.Domain.Models.Enums;
using StockBrain.Domain.Models.Extensions;

namespace StockBrain.Domain.Models;

public class InvestmentRecommendation
{
	public IEnumerable<InvestmentGroupCategory> Categories { get; }
	public IEnumerable<AssetMovement> Movements { get; }
	public IEnumerable<InvestmentGroup> Groups { get; }

	private IEnumerable<InvestmentGroup> CreateGroups()
	{
		var results = new List<InvestmentGroup>();

		foreach (var category in Categories)
		{
			results.Add(category);
			foreach (var type in category.Types)
			{
				results.Add(type);
				foreach (var asset in type.Assets)
				{
					results.Add(asset);
				}
			}
		}

		return results;
	}

	private IEnumerable<AssetMovement> CreateMovements()
	{
		var results = new List<AssetMovement>();

		foreach (var recommendation in Categories.SelectMany(c => c.Types.Where(t => t.Type.Category() == AssetCategory.Var).SelectMany(c => c.Assets)))
		{
			results.Add(new AssetMovement { 
				Asset = recommendation.Asset.Asset.Asset,
				Date = Date,
				Investment = recommendation.Investment.Value,
				Quantity = recommendation.Quantity.Value
			});
		}

		return results;
	}

	public double Investment { get; }
	public double NewTotal { get; }
	public double StartValue { get; }
	public DateOnly Date { get; }

	public InvestmentRecommendation()
	{
			
	}
	public InvestmentRecommendation(IEnumerable<InvestmentGroupType> types, double investment, double startValue, double newTotal, DateOnly date)
	{
		Investment = investment;
		StartValue = startValue;
		NewTotal = newTotal;
		Date = date;
		Categories = types.Where(t => t.Investment.Value > 0).GroupBy(t => t.Type.Category()).Select(g => new InvestmentGroupCategory(g.Key, g, investment, newTotal));
		Groups = CreateGroups();
		Movements = CreateMovements();
	}


}
