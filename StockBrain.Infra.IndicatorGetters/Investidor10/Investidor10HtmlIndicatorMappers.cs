using StockBrain.Infra.IndicatorGetters.Abstractions;

namespace StockBrain.Infra.IndicatorGetters.Investidor10;

public class Investidor10HtmlIndicatorMappers
{
	public static readonly IDictionary<Indicators, string> ChecklistValues = new Dictionary<Indicators, string>
	{
		 { Indicators.CompanyWithMoreThan5YearsOnStockMarket, "styled-checkbox-years" },
		 { Indicators.CompanyHasNeverPostedLossFiscalYear, "styled-checkbox-profitable" },
		 { Indicators.CompanyHasBeenProfitableLast20Quarters, "styled-checkbox-profitable5years" },
		 { Indicators.CompanyPaidMoreThan5PercentDividendsYear, "styled-checkbox-dy" },
		 { Indicators.CompanyHasRoeAbove10Percent, "styled-checkbox-roe" },
		 { Indicators.CompanyHasDebtLowerThanEquity, "styled-checkbox-debt" },
		 { Indicators.CompanyHasShownRevenueGrowthLast5Years, "styled-checkbox-cagrr5" },
		 { Indicators.CompanyHasShownProfitGrowthLast5Years, "styled-checkbox-cagrl" },
		 { Indicators.CompanyHasDailyLiquidityAbove2M, "styled-checkbox-liquidity" },
		 { Indicators.CompanyWellRatedByInvestidor10Users, "styled-checkbox-rating" }
	};
}
