using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Firebase.Services;

public class AssetDataCreator : IAssetDataCreator
{
	public AssetDataCreator(ISectors sectors, ISegments segments, IAccounts accounts, IAssets assets, IPortfolios portfolios, Context context)
	{
		Sectors = sectors;
		Segments = segments;
		Accounts = accounts;
		Assets = assets;
		Portfolios = portfolios;
		Context = context;
	}

	ISectors Sectors { get; }
	ISegments Segments { get; }
	IAccounts Accounts { get; }
	IAssets Assets { get; }
	IPortfolios Portfolios { get; }
	Context Context { get; }

	public void CreateAndAddToPortfolio(
		string ticker,
		string name,
		string description,
		DateOnly fundation,
		DateOnly ipo,
		string negativeNotes,
		string positiveNotes,
		string sectorName,
		string segmentName,
		AssetType type
	)
	{
		var asset = CreateAsset(ticker, name, description, fundation, ipo, negativeNotes, positiveNotes, sectorName, segmentName, type);
		foreach (var account in Accounts.All())
			AddOnAccount(account, asset);
	}
	AssetDTO CreateAsset(
		string ticker,
		string name,
		string description,
		DateOnly fundation,
		DateOnly ipo,
		string negativeNotes,
		string positiveNotes,
		string sectorName,
		string segmentName,
		AssetType type
	) 
	{
		return ((BaseFirebaseRepository<Asset, AssetDTO>)Assets).Save(new AssetDTO
		{
			GUID = ticker,
			Ticker = ticker,
			Name = name,
			Description = description,
			Foundation = fundation,
			IPO = ipo,
			NegativeNotes = negativeNotes,
			PositiveNotes = positiveNotes,
			Type = type,
			LastPriceUpdate = Context.Today,
			LastReview = Context.Today,
			SectorGUID = GetSector(sectorName).GUID,
			SegmentGUID = GetSegment(segmentName).GUID
		}, true);
	}
	void AddOnAccount(Account account, AssetDTO asset) 
	{
		Portfolios.ChangeAccount(account.GUID);
		foreach (var portfolio in Portfolios.All())
			AddOnPortfolio(portfolio, asset);
	}
	void AddOnPortfolio(Portfolio portfolio, AssetDTO asset)
	{
		var portifolioDTO = new PortfolioDTO(portfolio);
		portifolioDTO.Assets.Add(asset.Ticker, new PortfolioAssetDTO
		{
			GUID = asset.Ticker,
			Ticker = asset.Ticker,
			Quantity = 0,
			Value = 0,
			Risk = false,
			FirstAquisition = Context.Today,
			LastAquisition = Context.Today,
			Movements = new Dictionary<string, PortfolioAssetMovementDTO>(),
			Brokers = new Dictionary<string, PortfolioAssetBrokerDTO>()
		});
		((BaseFirebaseRepository<Portfolio, PortfolioDTO>)Portfolios).Save(portifolioDTO, true);
	}
	Sector GetSector(string sectorName)
	{
		var sector = Sectors.All().FirstOrDefault(s => s.Name == sectorName);
		if (sector == null)
			sector = Sectors.Save(new Sector { Name = sectorName });
		return sector;
	}
	Segment GetSegment(string segmentName)
	{
		var segment = Segments.All().FirstOrDefault(s => s.Name == segmentName);
		if (segment == null)
			segment = Segments.Save(new Segment { Name = segmentName });
		return segment;
	}
}
