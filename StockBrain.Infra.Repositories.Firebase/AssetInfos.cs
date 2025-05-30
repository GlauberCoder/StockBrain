using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enumerations;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Firebase;

public class AssetInfos : IAssetInfos
{
	IStockInfos StockInfos { get; }
	IREITInfos REITInfos { get; }
	IBDRInfos BDRInfos { get; }
	public AssetInfos(
		IStockInfos stockInfos,
		IREITInfos reitInfos,
		IBDRInfos bdrInfos)
	{
		StockInfos = stockInfos;
		REITInfos = reitInfos;
		BDRInfos = bdrInfos;
	}

	public IDictionary<string, AssetInfo> All()
	{
		var result = new Dictionary<string, AssetInfo>();

		foreach (var info in StockInfos.All())
			result.Add(info.Ticker, info);

		foreach (var info in REITInfos.All())
			result.Add(info.Ticker, info);

		foreach (var info in BDRInfos.All())
			result.Add(info.Ticker, info);

		return result;
	}

	public AssetInfo By(AssetType type, string ticker)
	{
		switch (type)
		{
			case AssetType.Acoes:
				return StockInfos.ByTicker(ticker);
			case AssetType.FII:
				return REITInfos.ByTicker(ticker);
			case AssetType.BDR:
				return BDRInfos.ByTicker(ticker);
			default:
				return null;
		}
	}
}
