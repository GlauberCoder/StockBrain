namespace StockBrain.Services.Abstrations;

public interface IPriceUpdater
{
	Task UpdateAll();
	Task UpdateMissing();
}
