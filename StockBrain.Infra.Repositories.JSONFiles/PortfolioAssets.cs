using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class PortfolioAssets : BaseJSONFIleRepository<PortfolioAsset, PortfolioAssetDTO>, IPortfolioAssets
{
	IAssets Assets { get; }
	IPortfolioAssetMovements PortfolioAssetMovements { get; }
	IPortfolioAssetBrokers AssetBrokers { get; }

	public PortfolioAssets(Context context, DataJSONFilesConfig config, IAssets assets, IPortfolioAssetMovements portfolioAssetMovements, IPortfolioAssetBrokers assetBrokers)
		: base(context, config, "portfolioAssets")
	{
		Assets = assets;
		PortfolioAssetMovements = portfolioAssetMovements;
		AssetBrokers = assetBrokers;
	}


	protected override PortfolioAsset FromDTO(PortfolioAssetDTO dto)
	{
		var asset = Assets.All().First(s => s.Ticker == dto.Ticker);
		var movements = PortfolioAssetMovements.ByPortfolio(dto.PortifolioID).Where(s => s.Ticker == dto.Ticker);
		var brokers = AssetBrokers.ByPortfolio(dto.PortifolioID).Where(d => d.Ticker == dto.Ticker);
		return dto.ToEntity(asset, movements, brokers,  Context);
	}

	protected override PortfolioAssetDTO FromEntity(PortfolioAsset entity) => new PortfolioAssetDTO(entity, Context.Account.ID);

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

			entities.Add(dto.ToEntity(assets[dto.Ticker], assetMovements, brokers[dto.PortifolioID][dto.Ticker], Context));
		}

		return entities;
	}

	protected override IEnumerable<PortfolioAssetDTO> FromEntity(IEnumerable<PortfolioAsset> entities) => entities.Select(FromEntity);

	public IEnumerable<PortfolioAsset> ByPortifolio(long portifolioID) => FromDTO(AllDTO().Where(a => a.PortifolioID == portifolioID));

}
