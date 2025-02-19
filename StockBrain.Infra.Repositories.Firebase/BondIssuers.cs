using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.FirebaseServices;

namespace StockBrain.Infra.Repositories.Firebase;

public class BondIssuers : BaseFirebaseRepository<BondIssuer, BondIssuer>, IBondIssuers
{
	public BondIssuers(Context context, FirebaseConfigModel config)
		: base(context, config, "bondIssuers")
	{
	}

	protected override BondIssuer FromDTO(BondIssuer dto) => dto;

	protected override IEnumerable<BondIssuer> FromDTO(IEnumerable<BondIssuer> dtos) => dtos;

	protected override BondIssuer FromEntity(BondIssuer entity) => entity;

	protected override IEnumerable<BondIssuer> FromEntity(IEnumerable<BondIssuer> entities) => entities;
}
