using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services.Abstrations;

namespace StockBrain.Services;

public class PortfolioAssetManager : IPortfolioAssetManager
{
	Context Context { get; }
	IPortfolios Portfolios { get; }
	IAssetMovements Movements { get; }
	IBondMovements BondMovements { get; }

	public PortfolioAssetManager(
		Context context,
		IPortfolios portfolios,
		IAssetMovements movements, 
		IBondMovements bondMovements
		)
	{
		Context = context;
		Portfolios = portfolios;
		Movements = movements;
		BondMovements = bondMovements;
	}
	public void ConfirmMovements(IEnumerable<EntityReference> portfolioReferences, IEnumerable<AssetMovement> assets, IEnumerable<BondMovement> bonds)
	{
		var portifolios = new List<Portfolio>();
		foreach (var portfolioReference in portfolioReferences)
		{
			var portfolio = Portfolios.ByID(portfolioReference.GUID);
			UpdateAssets(portfolio, assets);
			UpdateBonds(portfolio, bonds);
			portifolios.Add(portfolio);
		}
		Flush(portifolios, assets, bonds);

	}
	void UpdateBonds(Portfolio portfolio, IEnumerable<BondMovement> bonds)
	{
		var portfolioBonds = portfolio.Bonds.ToList();
		foreach (var bond in bonds)
			portfolioBonds.Add(bond.ToBond());

		portfolio.Bonds = portfolioBonds;
	}
	void UpdateAssets(Portfolio portfolio, IEnumerable<AssetMovement> assets)
	{
		var portfolioAssets = portfolio.Assets.ToList();
		foreach (var movement in assets)
		{
			var portfolioAssetDetail = portfolio.Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == movement.Asset.Ticker);
			if (portfolioAssetDetail == null)
			{
				portfolioAssetDetail = BuildNewAsset(movement.Asset);
				portfolioAssets.Add(portfolioAssetDetail);
			}
			ApplyMovement(portfolioAssetDetail, movement);
		}

		portfolio.Assets = portfolioAssets;
	}
	void ApplyMovement(PortfolioAssetDetail asset, AssetMovement movement) 
	{
		var assetMovement = ApplyAssetMovement(asset, movement);
		ApplyMovementOnAsset(asset, assetMovement);
		ApplyBrokerMovement(asset, movement);

	}
	PortfolioAssetMovement ApplyAssetMovement(PortfolioAssetDetail asset, AssetMovement movement) 
	{
		var assetMovement = new PortfolioAssetMovement(movement, asset.Asset, Context);
		var movements = asset.Asset.Movements.ToList();
		movements.Add(assetMovement);
		asset.Asset.Movements = movements;
		return assetMovement;
	}
	void ApplyMovementOnAsset(PortfolioAssetDetail asset, PortfolioAssetMovement assetMovement)
	{
		asset.Asset.Quantity = assetMovement.EndQuantity;
		asset.Asset.InvestedValue = assetMovement.EndInvestment;
		asset.Asset.LastAquisition = Context.Today;
	}
	void ApplyBrokerMovement(PortfolioAssetDetail asset, AssetMovement movement)
	{
		var brokers = asset.Asset.Brokers.ToList();

		var broker = brokers.FirstOrDefault(a => a.Broker.GUID == movement.Broker.GUID);
		if (broker == null)
		{
			broker = new PortfolioAssetBroker { Broker = movement.Broker, Quantity = 0, Ticker = asset.Asset.Asset.Ticker };
			brokers.Add(broker);
		}

		broker.Quantity += movement.Quantity;
		asset.Asset.Brokers = brokers;
	}
	PortfolioAssetDetail BuildNewAsset(Asset asset) 
	{
		var portfolioAsset = new PortfolioAsset
		{
			GUID = asset.GUID,
			Asset = asset,
			Quantity = 0,
			InvestedValue = 0,
			Risk = false,
			FirstAquisition = Context.Today,
			LastAquisition = Context.Today,
			Movements = new List<PortfolioAssetMovement>(),
			Brokers = new List<PortfolioAssetBroker>(),
		};
		return new PortfolioAssetDetail
		{
			Asset = portfolioAsset,
			InvestedOnTotal = null,
			InvestedType = null,
			Target = null,
			DeltaTarget = null,
		};
	}
	void Flush(IEnumerable<Portfolio> porfolios, IEnumerable<AssetMovement> assets, IEnumerable<BondMovement> bondMovements)
	{
		Portfolios.Save(porfolios);
		Movements.Delete(assets);
		BondMovements.Delete(bondMovements);
	}
}
