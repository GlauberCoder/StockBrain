using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezStockInfoGetter : InvestidorDezAssetInfoGetter<StockInfo, StockInfoMap>, IStockInfoGetter
{
	public InvestidorDezStockInfoGetter(Context context) : base(context)
	{
	}
}
