using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class BondIssuers : BaseJSONFIleRepository<BondIssuer, BondIssuer>, IBondIssuers
{
	public BondIssuers(Context context, DataJSONFilesConfig config)
		: base(context, config, "bondIssuers")
	{
	}

	protected override BondIssuer FromDTO(BondIssuer dto) => dto;

	protected override IEnumerable<BondIssuer> FromDTO(IEnumerable<BondIssuer> dtos) => dtos;

	protected override BondIssuer FromEntity(BondIssuer entity) => entity;

	protected override IEnumerable<BondIssuer> FromEntity(IEnumerable<BondIssuer> entities) => entities;
}
