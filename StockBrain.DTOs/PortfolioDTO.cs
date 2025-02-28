using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.DTOs;

public class PortfolioDTO : BaseEntity
{
	public PortfolioDTO()
	{

	}
	public PortfolioDTO(Portfolio portifolio)
	{
		Targets = portifolio.Types.ToDictionary(p => p.Key, p => p.Value.Target.Proportion);
		Name = portifolio.Name;
		Assets = portifolio.Assets.ToDictionary(a => a.Asset.Asset.Ticker, a => new PortfolioAssetDTO(a.Asset));
		Bonds = portifolio.Bonds.ToDictionary(a => a.GUID, a => new BondDTO(a));

	}
	public Dictionary<AssetType, double> Targets { get; set; }
	public string Name { get; set; }
	public IDictionary<string, PortfolioAssetDTO> Assets { get; set; }
	public IDictionary<string, BondDTO> Bonds { get; set; }

}
