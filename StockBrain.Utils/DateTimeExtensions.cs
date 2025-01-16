namespace StockBrain.Utils;

public static class DateTimeExtensions
{
	public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
	public static TimeSpan TimeSpanBetween(this DateTime start, DateOnly end) => start.TimeSpanBetween(end.ToDateTime());
	public static TimeSpan TimeSpanBetween(this DateTime start, DateTime end) => (start - end);
}
