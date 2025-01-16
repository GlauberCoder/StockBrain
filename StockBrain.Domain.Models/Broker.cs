namespace StockBrain.Domain.Models;

public class Broker : BaseEntity
{
	public required string Name { get; init; }
	public override bool Equals(object o)
	{
		var other = o as Broker;

		return other?.GUID == GUID;
	}

	public override string ToString() => Name;

	public override int GetHashCode() => base.GetHashCode();
}
