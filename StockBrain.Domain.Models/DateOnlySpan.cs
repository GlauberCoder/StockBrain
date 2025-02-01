using StockBrain.Utils;

namespace StockBrain.Domain.Models;

public class DateOnlySpan
{
	public DateOnlySpan(DateOnly date, DateOnly today)
	{
		Date = date;
		Span = today.TimeSpanBetween(date);
	}

	public DateOnly Date { get; }
	public TimeSpan Span { get; }

}
