using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.FirebaseServices;

namespace StockBrain.Infra.Repositories.Firebase;

public class Sectors : BaseFirebaseRepository<Sector, Sector>, ISectors
{
	public Sectors(Context context, FirebaseConfigModel config)
		: base(context, config, "sectors")
	{
	}

	protected override Sector FromDTO(Sector dto) => dto;

	protected override IEnumerable<Sector> FromDTO(IEnumerable<Sector> dtos) => dtos;

	protected override Sector FromEntity(Sector entity) => entity;

	protected override IEnumerable<Sector> FromEntity(IEnumerable<Sector> entities) => entities;
}
