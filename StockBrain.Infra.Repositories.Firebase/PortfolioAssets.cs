using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class PortfolioAssets : BaseFirebaseRepository<PortfolioAsset, PortfolioAssetDTO>, IPortfolioAssets
{
	IAssets Assets { get; }
	IPortfolioAssetMovements PortfolioAssetMovements { get; }
	IPortfolioAssetBrokers AssetBrokers { get; }
	IDecisionFactorAnswerSetter DecisionFactorsSetter { get; }
	IAssetInfos AssetInfos { get; }
	IDecisionFactors DecisionFactors { get; }

	public PortfolioAssets(
		Context context,
		DataBaseClient client, 
		IAssets assets, 
		IPortfolioAssetMovements portfolioAssetMovements, 
		IPortfolioAssetBrokers assetBrokers, 
		IDecisionFactorAnswerSetter decisionFactorSetter,
		IAssetInfos assetInfos,
		IDecisionFactors decisionFactors
		)
		: base(context, client, "portfolioAssets")
	{
		Assets = assets;
		PortfolioAssetMovements = portfolioAssetMovements;
		AssetBrokers = assetBrokers;
		DecisionFactorsSetter = decisionFactorSetter;
		AssetInfos = assetInfos;
		DecisionFactors = decisionFactors;
	}


	protected override PortfolioAsset FromDTO(PortfolioAssetDTO dto)
	{
		var asset = Assets.All().First(s => s.Ticker == dto.Ticker);
		var movements = PortfolioAssetMovements.ByPortfolio(dto.PortifolioID).Where(s => s.Ticker == dto.Ticker);
		var brokers = AssetBrokers.ByPortfolio(dto.PortifolioID).Where(d => d.Ticker == dto.Ticker);
		return dto.ToEntity(asset, movements, brokers,  Context);
	}

	protected override PortfolioAssetDTO FromEntity(PortfolioAsset entity) => new PortfolioAssetDTO(entity);

	protected override IEnumerable<PortfolioAsset> FromDTO(IEnumerable<PortfolioAssetDTO> dtos)
	{
		var assets = Assets.All().ToDictionary(s => s.Ticker, s => s);
		var movements = PortfolioAssetMovements
							.All()
							.GroupBy(a => a.PortfolioID)
							.ToDictionary(t => t.Key,
								t => t.GroupBy(a => a.Ticker).ToDictionary(t => t.Key, t => t.ToList())
							);
		var brokers = AssetBrokers
							.All()
							.GroupBy(a => a.PortfolioID)
							.ToDictionary(t => t.Key,
								t => t.GroupBy(a => a.Ticker).ToDictionary(t => t.Key, t => t.ToList())
							);

		var entities = new List<PortfolioAsset>();
		foreach (var dto in dtos)
		{
			var assetMovements = new List<PortfolioAssetMovement>();
			if (movements.TryGetValue(dto.PortifolioID, out var portfolioMovements))
				portfolioMovements.TryGetValue(dto.Ticker, out assetMovements);
			var assetBrokers = brokers.ContainsKey(dto.PortifolioID) && brokers[dto.PortifolioID].ContainsKey(dto.Ticker) ? brokers[dto.PortifolioID][dto.Ticker] : Enumerable.Empty<PortfolioAssetBroker>();
			var entity = dto.ToEntity(assets[dto.Ticker], assetMovements, assetBrokers, Context);
			entities.Add(entity);
		}
		DecisionFactorsSetter.Set(entities,AssetInfos.All(), DecisionFactors.All());
		return entities;
	}

	protected override IEnumerable<PortfolioAssetDTO> FromEntity(IEnumerable<PortfolioAsset> entities) => entities.Select(FromEntity);

	public IEnumerable<PortfolioAsset> ByPortifolio(long portifolioID) => FromDTO(AllDTO().Where(a => a.PortifolioID == portifolioID));

}
