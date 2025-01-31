using StockBrain.Domain.Models;

namespace StockBrain.InvestidorDez.Mapper;

public class StockInfoMap : AssetInfoMap<StockInfo>
{
	public StockInfoMap()
	{
		Map(s => s.Price, "//section[@id='cards-ticker']/div[@class='_card cotacao']/div[@class='_card-body']/div/span", false);
		Map(s => s.Debt, "//div[@id='table-indicators-company']/div[7]/span[2]/div[@class='detail-value']", false);
		Map(s => s.Equity, "//div[@id='table-indicators-company']/div[3]/span[2]/div[@class='detail-value']", false);
		Map(s => s.DailyLiquidity, "//div[@id='table-indicators-company']/div[13]/span[2]/div[@class='detail-value']", false);
		Map(s => s.ROE, "//div[@id='table-indicators']/div[20]/div[1]/span", false);
		Map(s => s.LPA, "//div[@id='table-indicators']/div[18]/div[1]/span", false);
		Map(s => s.VPA, "//div[@id='table-indicators']/div[17]/div[1]/span", false);
		Map(s => s.RevenueCAGR, "//div[@id='table-indicators']/div[30]/div[1]/span", false);
		Map(s => s.ProfitCAGR, "//div[@id='table-indicators']/div[31]/div[1]/span", false);
		MapCheckbox(s => s.HasNeverPostedLosses, "styled-checkbox-profitable", true);
		MapCheckbox(s => s.ProfitableLastQuarters, "styled-checkbox-profitable5years", true);
		MapCheckbox(s => s.PaidAcceptableDividends, "styled-checkbox-dy", true);
		MapCheckbox(s => s.WellRated, "styled-checkbox-rating", true);
	}
}
