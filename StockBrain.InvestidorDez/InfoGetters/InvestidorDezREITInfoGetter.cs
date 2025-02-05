using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.Mapper;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezREITInfoGetter : InvestidorDezAssetInfoGetter<REITInfo>
{
	public InvestidorDezREITInfoGetter(Context context, InvestidorDezClient client) : base(context, client)
	{
	}

	protected override AssetInfoMap<REITInfo> GetMap() => new REITInfoMap();
}
