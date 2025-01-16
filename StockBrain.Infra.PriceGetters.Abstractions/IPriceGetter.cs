namespace StockBrain.Infra.PriceGetters.Abstractions;

public interface IPriceGetter
{
	Task<double?> Get(string ticker);
}
