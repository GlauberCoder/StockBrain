using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Clients;
using StockBrain.InvestidorDez.Mapper;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezBDRInfoGetter : InvestidorDezAssetInfoGetter<BDRInfo>
{
	public InvestidorDezBDRInfoGetter(Context context, InvestidorDezClient client) : base(context, client)
	{
	}

	protected override AssetInfoMap<BDRInfo> GetMap() => new BDRInfoMap();
}
