namespace StockBrain.Infra.IndicatorGetters.Abstractions.Investidor10;

public interface IIndicatorGetter
{
	Task<IDictionary<Indicators, bool>> Get(string ticker);
}
