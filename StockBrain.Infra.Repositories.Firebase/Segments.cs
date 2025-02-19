using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.FirebaseServices;

namespace StockBrain.Infra.Repositories.Firebase;

public class Segments : BaseFirebaseRepository<Segment, Segment>, ISegments
{
	public Segments(Context context, FirebaseConfigModel config)
		: base(context, config, "segments")
	{
	}

	protected override Segment FromDTO(Segment dto) => dto;
	protected override IEnumerable<Segment> FromDTO(IEnumerable<Segment> dtos) => dtos;
	protected override Segment FromEntity(Segment entity) => entity;
	protected override IEnumerable<Segment> FromEntity(IEnumerable<Segment> entities) => entities;
}
