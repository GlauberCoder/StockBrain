namespace StockBrain.Utils;

public static class DoubleExtensions
{
    public static double ToPrecision(this double number, int precision = 2) => double.IsNaN(number) ? 0 : (double)Math.Round((decimal)number, precision);
    public static double ToPercentage(this double number, int precision = 0) => double.IsNaN(number) ? 0 : (double)Math.Round((decimal)number * 100, precision);
    public static string PercentageFormat(this double number, int precision = 0) => $"{number.ToPercentage(precision)}%";
	public static string MonetaryFormat(this double value) => String.Format(new System.Globalization.CultureInfo("pt-Br"), "{0:C}", value);
	public static string MonetaryThousandFormat(this double value) => $"{(value/1000).ToPrecision(0)} k";
}
