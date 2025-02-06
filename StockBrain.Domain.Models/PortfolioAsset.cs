using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class PortfolioAsset : BaseEntity
{
	public long PortfolioID { get; set; }
	public Asset Asset { get; set; }
	public int Quantity { get; set; }
	public double InvestedValue { get; set; }
	public bool Risk { get; set; }
	public DateOnly FirstAquisition { get; set; }
	public DateOnly LastAquisition { get; set; }
	public IEnumerable<PortfolioAssetMovement> Movements { get; set; }
	public IEnumerable<PortfolioAssetBroker> Brokers { get; set; }
	public double AveragePrice => (InvestedValue / Quantity).ToPrecision(2);
	public double CurrentValue => ((Asset.MarketPrice ?? 0) * Quantity).ToPrecision(2);
	public DeltaValue DeltaPrice => new DeltaValue(AveragePrice, Asset.MarketPrice ?? 0);
	public DeltaValue DeltaTotal => new DeltaValue(InvestedValue, CurrentValue);
	public IEnumerable<DecisionFactorAnswer> Answers { get; private set; }
	public PercentageValue Score { get; private set; }
	public void SetScore(IEnumerable<DecisionFactorAnswer> answers) 
	{
		var max = answers.Count();
		Answers = answers;
		var points = Math.Max(answers.Sum(a => a.Answer ? 1 : -1), 0);
		Score = new PercentageValue(points, max);
	}

}
