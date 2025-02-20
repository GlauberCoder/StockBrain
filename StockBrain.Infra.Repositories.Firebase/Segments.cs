using FireSharp.Interfaces;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Firebase;

public class Segments : BaseFirebaseRepository<Segment, Segment>, ISegments
{
	public Segments(Context context, IFirebaseClient client)
		: base(context, client, "segments")
	{
	}

	protected override Segment FromDTO(Segment dto) => dto;
	protected override IEnumerable<Segment> FromDTO(IEnumerable<Segment> dtos) => dtos;
	protected override Segment FromEntity(Segment entity) => entity;
	protected override IEnumerable<Segment> FromEntity(IEnumerable<Segment> entities) => entities;
}
