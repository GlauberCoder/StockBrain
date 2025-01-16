using System;

namespace StockBrain.Utils;

public static class TimeSpanExtensions
{
	public static int Years(this TimeSpan span) => (int)(span.TotalDays / 365.25);
	public static int Months(this TimeSpan span) {
		int years = (int)(span.TotalDays / 365.25);
		double remainingDays = span.TotalDays - (years * 365.25);
		return (int)(remainingDays / 30.44);
	}
	public static string YearMonthFormat(this TimeSpan span) 
	{
		var years = span.Years();
		var months = span.Months();
		var yearText = years > 0 ? (years > 1 ? $"{years} anos" : $"{years} ano") : string.Empty;
		var monthText = months > 0 ? (months > 1 ? $"{months} meses" : $"{months} mês") : string.Empty;

		return yearText + (string.IsNullOrEmpty(yearText) || string.IsNullOrEmpty(monthText) ? monthText : $" e {monthText}");
	}
}
