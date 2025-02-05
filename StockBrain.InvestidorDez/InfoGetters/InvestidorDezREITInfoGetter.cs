using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezREITInfoGetter : InvestidorDezAssetInfoGetter<REITInfo, REITInfoMap>, IREITInfoGetter
{
	public InvestidorDezREITInfoGetter(Context context) : base(context)
	{
	}
}
