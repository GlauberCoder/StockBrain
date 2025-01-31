using Newtonsoft.Json;

namespace StockBrain.InvestidorDez.Models;

public class ValueYear
{
	[JsonProperty("price")]
	public double Value { get; set; }
	[JsonProperty("created_at")]
	public int Year { get; set; }
}
