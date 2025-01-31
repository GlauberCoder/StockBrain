using Newtonsoft.Json;

namespace StockBrain.InvestidorDez.Models;

public class ValueDate
{
	[JsonProperty("price")]
	public double Value { get; set; }
	[JsonProperty("created_at")]
	public DateTime Date { get; set; }
}
