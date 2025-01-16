using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Segments : BaseJSONFIleRepository<Segment, Segment>, ISegments
{
	public Segments(Context context, DataJSONFilesConfig config)
		: base(context, config, "segments")
	{
	}

	protected override Segment FromDTO(Segment dto) => dto;
	protected override IEnumerable<Segment> FromDTO(IEnumerable<Segment> dtos) => dtos;
	protected override Segment FromEntity(Segment entity) => entity;
	protected override IEnumerable<Segment> FromEntity(IEnumerable<Segment> entities) => entities;
}
