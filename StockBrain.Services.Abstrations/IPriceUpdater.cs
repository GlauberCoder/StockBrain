using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPriceUpdater
{
	Task Update(Action<IDictionary<string, IAssetInfoUpdateStatus>, bool> onUpdate = null, IEnumerable<string> tickersFilter = null);
}
