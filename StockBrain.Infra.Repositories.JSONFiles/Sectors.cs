using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Sectors : BaseJSONFIleRepository<Sector, Sector>, ISectors
{
	public Sectors(Context context, DataJSONFilesConfig config)
		: base(context, config, "sectors")
	{
	}

	protected override Sector FromDTO(Sector dto) => dto;

	protected override IEnumerable<Sector> FromDTO(IEnumerable<Sector> dtos) => dtos;

	protected override Sector FromEntity(Sector entity) => entity;

	protected override IEnumerable<Sector> FromEntity(IEnumerable<Sector> entities) => entities;
}
