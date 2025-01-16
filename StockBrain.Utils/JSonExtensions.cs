using Newtonsoft.Json;

namespace StockBrain.Utils;

public static class JSonExtensions
{
	public static string Serialize(this object data) => JsonConvert.SerializeObject(data, Formatting.Indented);
	public static T Deserialize<T>(this string data) => JsonConvert.DeserializeObject<T>(data);
}
