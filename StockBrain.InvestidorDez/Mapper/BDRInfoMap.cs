using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.InvestidorDez.Mapper;

public class BDRInfoMap : AssetInfoMap<BDRInfo>
{
	public BDRInfoMap()
	{
		Map(s => s.Price, "//section[@id='cards-ticker']/div[@class='_card cotacao']/div[@class='_card-body']/div/span", false);
		Map(s => s.Equity, Company(3), false);
		Map(s => s.ROE, Indicator(6), false);
		Map(s => s.LPA, Indicator(15), false);
		Map(s => s.VPA, Indicator(14), false);
		MapCheckbox(s => s.HasNeverPostedLosses, "styled-checkbox-profitable", true);
		MapCheckbox(s => s.ProfitableLastQuarters, "styled-checkbox-profitable5years", true);
		MapCheckbox(s => s.WellRated, "styled-checkbox-rating", true);
	}
	string Indicator(int div) =>  $"//div[@id='table-indicators']/div[{div}]/div[1]/span";
	string Company(int div) => $"//div[@id='table-indicators-company']/div[{div}]/span[2]/div[@class='detail-value']";
}
