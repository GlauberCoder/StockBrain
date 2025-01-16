using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Stubs;

public class Assets : IAssets
{
	Context Context { get; }
	ISectors Sectors { get; }
	ISegments Segments { get; }
	public Assets(Context context, ISectors sectors, ISegments segments)
	{
		Context = context;
		Sectors = sectors;
		Segments = segments;
	}


	public IEnumerable<Asset> All()
	{
		return new List<Asset> {
			new Asset
			{
				ID = 1,
				Name = "Raia Drogasil",
				Ticker = "RADL3",
				Type = AssetType.Stock,
				SectorID = 1, //Saúde
				SegmentID = 1,//Serviços Médicos, Hospitalares, Análises e Diagnósticos
			},
			new Asset
			{
				ID = 2,
				Name = "Fleury",
				Ticker = "FLRY3",
				Type = AssetType.Stock,
				SectorID = 1, //Saúde
				SegmentID = 2,//Medicamentos
			},
			new Asset
			{
				ID = 3,
				Name = "XP MALL",
				Ticker = "XPML11",
				Type = AssetType.FII,
				SectorID = 100, //Tijolo
				SegmentID = 100,//Shoppings
			},

		};
	}
	public IEnumerable<AssetWithDetail> AllWithDetails()
	{
		var sectors = Sectors.All().ToDictionary( s => s.ID, s => s);
		var segments = Segments.All().ToDictionary( s => s.ID, s => s);
		var assests = All();
		var assetsWithDetails = new List<AssetWithDetail>();
		foreach (var asset in assests)
		{
			assetsWithDetails.Add(
				new AssetWithDetail 
				{ 
					Asset = asset, 
					Sector = sectors[asset.SectorID], 
					Segment = segments[asset.SegmentID]
				}
			);
		}
		return assetsWithDetails;
	}
}
