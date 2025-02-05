using Newtonsoft.Json;

namespace StockBrain.InvestidorDez.Models;

public class ValueDate
{
	[JsonProperty("price")]
	public double Value { get; set; }
	[JsonProperty("created_at")]
	public DateTime Date { get; set; }
	public ValueDate()
	{

	}
	public ValueDate(ValueYear valueYear)
	{
		Value = valueYear.Value;
		Date = new DateTime(valueYear.Year, 1, 1);
	}
}
