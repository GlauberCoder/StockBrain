using System.ComponentModel;

namespace StockBrain.Infra.IndicatorGetters.Abstractions;

public enum Indicators
{
	[Description("Company with more than 5 years on the stock market")]
	CompanyWithMoreThan5YearsOnStockMarket,

	[Description("Company has never posted a loss (fiscal year)")]
	CompanyHasNeverPostedLossFiscalYear,

	[Description("Company has been profitable in the last 20 quarters (5 years)")]
	CompanyHasBeenProfitableLast20Quarters,

	[Description("Company paid more than 5% dividends per year in the last 5 years")]
	CompanyPaidMoreThan5PercentDividendsYear,

	[Description("Company has ROE above 10%")]
	CompanyHasRoeAbove10Percent,

	[Description("Company has debt lower than equity")]
	CompanyHasDebtLowerThanEquity,

	[Description("Company has shown revenue growth in the last 5 years")]
	CompanyHasShownRevenueGrowthLast5Years,

	[Description("Company has shown profit growth in the last 5 years")]
	CompanyHasShownProfitGrowthLast5Years,

	[Description("Company has daily liquidity above R$ 2M")]
	CompanyHasDailyLiquidityAbove2M,

	[Description("Company is well-rated by Investidor10 users")]
	CompanyWellRatedByInvestidor10Users
}
