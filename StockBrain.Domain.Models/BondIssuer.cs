namespace StockBrain.Domain.Models;

public class BondIssuer : BaseEntity
{
	public string Name { get; set; }
	public override bool Equals(object o)
	{
		var other = o as Asset;

		return other?.GUID == GUID;
	}

	public override string ToString() => Name;

	public override int GetHashCode() => base.GetHashCode();
}
