using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Models;

public class Bond : BaseEntity
{
	public Broker Broker { get; init; }
	public BondIssuer Issuer { get; init; }
	public BondType Type { get; init; }
	public BondIndex Index { get; init; }
	public double Tax { get; init; }
	public double Value { get; init; }
	public DateOnly Date { get; init; }
	public DateOnly Expiration { get; init; }
	public TimeSpan Age { get; init; }
	public TimeSpan TimeToExpire { get; init; }
	public DateOnly? Redeem { get; init; }
	public bool Expired { get; init; }
	public AssetType AssetType
	{
		get
		{
			return Type switch
			{
				BondType.Gov => AssetType.Gov,
				_ => AssetType.Priv
			};
		}
	}
	public bool Redeemed => Redeem.HasValue;
	public bool Active => !Redeemed && !Expired;
}
