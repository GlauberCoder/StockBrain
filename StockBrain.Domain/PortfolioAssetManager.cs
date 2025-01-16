using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Domain;

public class PortfolioAssetManager : IPortfolioAssetManager
{
	Context Context { get; }
	IPortfolioAssets PortfolioAssets { get; }
	IPortfolioAssetBrokers AssetBrokers { get; }
	IAssetMovements Movements { get; }
	IPortfolioAssetMovements PortfolioAssetMovements { get; }
	IBonds Bonds { get; }
	IBondMovements BondMovements { get; }

	public PortfolioAssetManager(
		Context context, 
		IPortfolioAssets portfolioAssets, 
		IPortfolioAssetBrokers assetBrokers, 
		IAssetMovements movements, 
		IPortfolioAssetMovements portfolioAssetMovements,
		IBonds bonds,
		IBondMovements bondMovements
		)
	{
		Context = context;
		PortfolioAssets = portfolioAssets;
		AssetBrokers = assetBrokers;
		Movements = movements;
		PortfolioAssetMovements = portfolioAssetMovements;
		Bonds = bonds;
		BondMovements = bondMovements;
	}
	public void ConfirmMovements(IEnumerable<Portfolio> portfolios, IEnumerable<AssetMovement> assets, IEnumerable<BondMovement> bonds)
	{
		var changedAssets = new List<PortfolioAsset>();
		var brokers = new List<PortfolioAssetBroker>();
		var assetMovements = new List<PortfolioAssetMovement>();
		var addedBonds = new List<Bond>();
		foreach (var portfolio in portfolios)
		{
			foreach (var movement in assets)
			{
				var asset = GetAsset(portfolio, movement.Asset);
				changedAssets.Add(asset);
				brokers.Add(GetAssetBroker(portfolio, movement, asset));
				assetMovements.Add(GetPortfolioAssetMovement(portfolio, movement, asset));
			}
			addedBonds.AddRange(bonds.Select(b => b.ToBond(portfolio.ID)));
		}
		Flush(assets, changedAssets, brokers, assetMovements, bonds, addedBonds);

	}

	void Flush(IEnumerable<AssetMovement> assets, IEnumerable<PortfolioAsset> changedAssets, IEnumerable<PortfolioAssetBroker> brokers, IEnumerable<PortfolioAssetMovement> assetMovements, IEnumerable<BondMovement> bondMovements, IEnumerable<Bond> bonds)
	{
		PortfolioAssets.Save(changedAssets);
		AssetBrokers.Save(brokers);
		Movements.Delete(assets);
		PortfolioAssetMovements.Save(assetMovements);
		Bonds.Save(bonds);
		BondMovements.Delete(bondMovements);
	}

	PortfolioAsset GetAsset(Portfolio portfolio, Asset asset)
	{

		var portfolioAsset = portfolio.Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == asset.Ticker)?.Asset;
		if (portfolioAsset == null)
		{
			portfolioAsset = new PortfolioAsset
			{
				PortfolioID = portfolio.ID,
				Asset = asset,
				Quantity = 0,
				InvestedValue = 0,
				Risk = false,
				FirstAquisition = Context.Today,
				LastAquisition = Context.Today,
				Movements = new List<PortfolioAssetMovement>(),
				Brokers = new List<PortfolioAssetBroker>(),
			};
		}
		return portfolioAsset;
	}
	PortfolioAssetMovement GetPortfolioAssetMovement(Portfolio portfolio, AssetMovement movement, PortfolioAsset asset)
	{
		var assetMovement = new PortfolioAssetMovement(movement, asset, Context);
		asset.Quantity = assetMovement.EndQuantity;
		asset.InvestedValue = assetMovement.EndInvestment;
		asset.LastAquisition = Context.Today;
		return assetMovement;
	}
	PortfolioAssetBroker GetAssetBroker(Portfolio portfolio, AssetMovement movement, PortfolioAsset asset)
	{
		var broker = asset.Brokers.FirstOrDefault(b => b.PortfolioID == portfolio.ID && b.Broker.ID == movement.Broker.ID);
		if (broker == null)
			broker = new PortfolioAssetBroker { Broker = movement.Broker, PortfolioID = portfolio.ID, Quantity = 0, Ticker = asset.Asset.Ticker };
		broker.Quantity += movement.Quantity;
		return broker;

	}
}
