using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class BondIssuers : BaseFirebaseRepository<BondIssuer, BondIssuer>, IBondIssuers
{
	public BondIssuers(Context context, DBClient client)
		: base(context, client, "bondIssuers")
	{
	}

	protected override BondIssuer FromDTO(BondIssuer dto) => dto;

	protected override IEnumerable<BondIssuer> FromDTO(IEnumerable<BondIssuer> dtos) => dtos;

	protected override BondIssuer FromEntity(BondIssuer entity) => entity;

	protected override IEnumerable<BondIssuer> FromEntity(IEnumerable<BondIssuer> entities) => entities;
}
