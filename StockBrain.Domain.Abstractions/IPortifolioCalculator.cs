using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Abstractions;

public interface IPortifolioCalculator
{
	Portfolio Calc(BaseEntity portifolio, Dictionary<AssetType, double> Targets, string name, bool main, IEnumerable<PortfolioAsset> assets, IEnumerable<Bond> bonds);
}
