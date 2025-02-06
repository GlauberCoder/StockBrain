namespace StockBrain.Services.Abstrations;

public interface IAssetInfoUpdater
{
	Task UpdateAll(Action<IDictionary<string, IAssetInfoUpdateStatus>, bool>  onUpdate = null, IEnumerable<string> tickersFilter = null);
}
