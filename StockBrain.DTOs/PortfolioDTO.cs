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
		AccountID = portifolio.AccountID;
		Targets = portifolio.Types.ToDictionary(p => p.Key, p => p.Value.Target.Proportion);
		Name = portifolio.Name;
		Main = portifolio.Main;

	}
	public long AccountID { get; set; }
	public Dictionary<AssetType, double> Targets { get; set; }
	public string Name { get; set; }
	public bool Main { get; set; }

}
