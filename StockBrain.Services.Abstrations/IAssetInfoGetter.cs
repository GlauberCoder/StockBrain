using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IAssetInfoGetter
{
	Task<StockInfo> GetStock(Asset asset);
	Task<IEnumerable<StockInfo>> GetStock(IEnumerable<Asset> assets);

}
