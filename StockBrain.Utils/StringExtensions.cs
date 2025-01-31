using System.Linq.Expressions;
using System.Reflection;

namespace StockBrain.Utils;

public static class StringExtensions
{
	public static string Remove(this string text, string value) => text.Replace(value, string.Empty);
	public static string ToPointDecimalSeparator(this string text) => text.Replace(".", string.Empty).Replace(",", ".");
	public static double ToDouble(this string text) => double.Parse(text.ToPointDecimalSeparator().Trim());
}
