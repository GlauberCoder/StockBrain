namespace StockBrain.Utils;

public static class DateOnlyExtensions
{
	public static DateTime ToDateTime(this DateOnly date) => date.ToDateTime(new TimeOnly());
	public static TimeSpan TimeSpanBetween(this DateOnly start, DateOnly end) => start.TimeSpanBetween(end.ToDateTime());
	public static TimeSpan TimeSpanBetween(this DateOnly start, DateTime end) => (start.ToDateTime() - end);
}
