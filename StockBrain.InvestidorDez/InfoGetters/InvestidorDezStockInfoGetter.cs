using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.Mapper;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezStockInfoGetter : InvestidorDezAssetInfoGetter<StockInfo>
{
	public InvestidorDezStockInfoGetter(Context context, InvestidorDezClient client) : base(context, client)
	{
	}

	protected override AssetInfoMap<StockInfo> GetMap() => new StockInfoMap();
}
