﻿using StockBrain.Domain.Models;

namespace StockBrain.Infra.Repositories.Abstractions;

public interface IAssets : IBaseRepository<Asset>
{
	public Asset SaveEvaluation(Asset asset);
	public Asset ByTicker(string ticker);
}
