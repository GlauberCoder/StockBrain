using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.InvestidorDez.Mapper;

public class StockInfoMap : AssetInfoMap<StockInfo>
{

	public StockInfoMap()
	{
		Map(s => s.Price, "//section[@id='cards-ticker']/div[@class='_card cotacao']/div[@class='_card-body']/div/span", false);
		Map(s => s.Debt, Company(7), false);
		Map(s => s.Equity, Company(3), false);
		Map(s => s.DailyLiquidity, Company(13), false);
		Map(s => s.ROE, Indicator(20), false);
		Map(s => s.LPA, Indicator(18), false);
		Map(s => s.VPA, Indicator(17), false);
		Map(s => s.RevenueCAGR, Indicator(30), false);
		Map(s => s.ProfitCAGR, Indicator(31), false);
		MapCheckbox(s => s.HasNeverPostedLosses, "styled-checkbox-profitable", true);
		MapCheckbox(s => s.ProfitableLastQuarters, "styled-checkbox-profitable5years", true);
		MapCheckbox(s => s.PaidAcceptableDividends, "styled-checkbox-dy", true);
		MapCheckbox(s => s.WellRated, "styled-checkbox-rating", true);
	}
	string Indicator(int div) =>  $"//div[@id='table-indicators']/div[{div}]/div[1]/span";
	string Company(int div) => $"//div[@id='table-indicators-company']/div[{div}]/span[2]/div[@class='detail-value']";
}
