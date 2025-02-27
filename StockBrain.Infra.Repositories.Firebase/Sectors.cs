using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class Sectors : BaseFirebaseRepository<Sector, Sector>, ISectors
{
	public Sectors(Context context, DataBaseClient client)
		: base(context, client, "sectors")
	{
	}

	protected override Sector FromDTO(Sector dto) => dto;

	protected override IEnumerable<Sector> FromDTO(IEnumerable<Sector> dtos) => dtos;

	protected override Sector FromEntity(Sector entity) => entity;

	protected override IEnumerable<Sector> FromEntity(IEnumerable<Sector> entities) => entities;
}
