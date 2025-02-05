namespace StockBrain.Services.Abstrations;

public interface IAssetInfoUpdater
{
	Task UpdateAll(Action<IDictionary<string, IAssetInfoUpdateStatus>> callback, IEnumerable<string> tickersFilter = null);
}
