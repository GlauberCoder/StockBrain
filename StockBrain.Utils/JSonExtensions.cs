using Newtonsoft.Json;
using System.Globalization;

namespace StockBrain.Utils;

public static class JSonExtensions
{
	public static string Serialize(this object data) => JsonConvert.SerializeObject(data, Formatting.Indented);
	public static T Deserialize<T>(this string data, JsonSerializerSettings settings = null)
	{
		return JsonConvert.DeserializeObject<T>(data, settings);
	}
}
