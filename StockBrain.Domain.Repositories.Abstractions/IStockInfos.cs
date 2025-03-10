﻿using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IStockInfos :  IBaseRepository<StockInfo>
{
	StockInfo ByTicker(string ticker);
}
