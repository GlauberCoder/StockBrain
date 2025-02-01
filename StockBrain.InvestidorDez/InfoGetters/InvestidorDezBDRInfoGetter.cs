using StockBrain.Domain.Models.AssetInfos;
using StockBrain.InvestidorDez.Mapper;
using StockBrain.Services.Abstrations;

namespace StockBrain.InvestidorDez.InfoGetters;

public class InvestidorDezBDRInfoGetter : InvestidorDezAssetInfoGetter<BDRInfo, BDRInfoMap>, IBDRInfoGetter
{

}
