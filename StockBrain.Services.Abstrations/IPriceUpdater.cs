using StockBrain.Domain.Models;

namespace StockBrain.Services.Abstrations;

public interface IPriceUpdater
{
	Task UpdateAll(Action<IEnumerable<Asset>> onFinish);
	Task UpdateMissing(Action<IEnumerable<Asset>> onFinish);
}
