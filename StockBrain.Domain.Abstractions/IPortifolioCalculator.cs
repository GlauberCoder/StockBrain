using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.Domain.Abstractions;

public interface IPortifolioCalculator
{
	Portfolio Calc(BaseEntity portifolio, long accountID, Dictionary<AssetType, double> Targets, string name, bool main, IEnumerable<PortfolioAsset> assets, IEnumerable<Bond> bonds);
}
