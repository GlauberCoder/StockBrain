using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class Assets(Context context, DBClient client, ISectors sectors, ISegments segments)
		: BaseFirebaseRepository<Asset, AssetDTO>(context, client, "assets"), IAssets
{
	ISectors Sectors { get; } = sectors;
	ISegments Segments { get; } = segments;

	protected override Asset FromDTO(AssetDTO dto)
	{
		var sector = Sectors.ByID(dto.SectorGUID);
		var segment = Segments.ByID(dto.SegmentGUID);

		return dto.ToAsset(sector, segment, Context);
	}

	protected override AssetDTO FromEntity(Asset entity) => new AssetDTO(entity);

	protected override IEnumerable<Asset> FromDTO(IEnumerable<AssetDTO> dtos)
	{
		var sectors = Sectors.All().ToDictionary(s => s.GUID, s => s);
		var segments = Segments.All().ToDictionary(s => s.GUID, s => s);
;		var assets = new List<Asset>();
		foreach (var dto in dtos)
		{
			assets.Add(dto.ToAsset(sectors[dto.SectorGUID], segments[dto.SegmentGUID], Context));
		}
		return assets;
	}

	protected override IEnumerable<AssetDTO> FromEntity(IEnumerable<Asset> entities) => entities.Select(FromEntity);


	public Asset ByTicker(string ticker) => FromDTO(AllDTO().First(a => a.Ticker == ticker));
}
