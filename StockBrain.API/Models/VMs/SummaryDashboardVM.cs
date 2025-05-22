using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.API.Models.VMs;

public class SummaryDashboardVM
{
	public string Name { get; set; }
	public double Total { get;  }
	public IDictionary<AssetCategory, PortfolioAssetGroup> Categories { get; }
	public IDictionary<AssetType, PortfolioAssetGroup> Types { get; }
	public SummaryDashboardVM(Portfolio portfolio)
	{
		Total = portfolio.Total; 
		Categories = portfolio.Categories;
		Types = portfolio.Types;
	}
}
