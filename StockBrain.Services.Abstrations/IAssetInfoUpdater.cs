using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IAssetInfoUpdater
{
	Task UpdateAll(Action<IDictionary<string, IAssetInfoUpdateStatus>, bool>  onUpdate = null, IEnumerable<string> tickersFilter = null);
	Task<IAssetInfoUpdateStatus> Update(Asset asset);
	Task<IAssetInfoUpdateStatus> Update(string ticker);
}
