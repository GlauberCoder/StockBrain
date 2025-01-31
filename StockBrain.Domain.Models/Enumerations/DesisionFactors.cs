namespace StockBrain.Domain.Models.Enumerations;

public class DesisionFactors
{
	public static DecisionFactor<StockStats> HasEnoughYearsInMarket = new DecisionFactor<StockStats>
	{
		Key = "HasEnoughYearsInMarket",
		Name = "Empresa com mais de 10 anos de Bolsa?",
		Description = "Favorece empresas que estão há mais de 10 anos no mercado, indicando estabilidade e histórico sólido.",
		Evaluator = s => s.HasEnoughYearsInMarket
	};

	public static DecisionFactor<StockStats> HasNeverPostedLosses = new DecisionFactor<StockStats>
	{
		Key = "HasNeverPostedLosses",
		Name = "Empresa nunca deu prejuízo (ano fiscal)?",
		Description = "Favorece empresas que consistentemente geram lucros e não registraram prejuízos.",
		Evaluator = s => s.Info.HasNeverPostedLosses
	};

	public static DecisionFactor<StockStats> ProfitableLastQuarters = new DecisionFactor<StockStats>
	{
		Key = "ProfitableLastQuarters",
		Name = "Empresa com lucro nos últimos trimestres?",
		Description = "Indica empresas que mantêm um histórico consistente de lucro trimestral.",
		Evaluator = s => s.Info.ProfitableLastQuarters
	};

	public static DecisionFactor<StockStats> PaidAcceptableDividends = new DecisionFactor<StockStats>
	{
		Key = "PaidAcceptableDividends",
		Name = "Empresa pagou dividendos aceitáveis nos últimos anos?",
		Description = "Distribuir dividendos é um indicativo de lucro real, favorece empresas que mantêm uma política de dividendos atrativa ao longo dos últimos anos.",
		Evaluator = s => s.Info.PaidAcceptableDividends
	};

	public static DecisionFactor<StockStats> HasAcceptableROE = new DecisionFactor<StockStats>
	{
		Key = "HasAcceptableROE",
		Name = "Empresa possui ROE aceitável",
		Description = "Favorece empresas com bom retorno sobre o patrimônio, mostrando eficiência em gerar lucros.",
		Evaluator = s => s.HasAcceptableROE
	};

	public static DecisionFactor<StockStats> LowDebtToEquity = new DecisionFactor<StockStats>
	{
		Key = "LowDebtToEquity",
		Name = "Empresa possui dívida menor que o patrimônio?",
		Description = "Indica empresas com menor risco financeiro devido a um endividamento controlado.",
		Evaluator = s => s.LowDebtToEquity
	};

	public static DecisionFactor<StockStats> PositiveRevenueCAGR = new DecisionFactor<StockStats>
	{
		Key = "PositiveRevenueCAGR",
		Name = "Empresa apresentou crescimento de receita?",
		Description = "Verifica empresas com um crescimento sustentável e consistente da receita.",
		Evaluator = s => s.PositiveRevenueCAGR
	};

	public static DecisionFactor<StockStats> ProfitGrowth = new DecisionFactor<StockStats>
	{
		Key = "ProfitGrowth",
		Name = "Empresa apresentou crescimento de lucros nos últimos 5 anos?",
		Description = "Favorece empresas que demonstraram aumento consistente nos lucros ao longo do tempo.",
		Evaluator = s => s.PositiveProfitCAGR
	};

	public static DecisionFactor<StockStats> HasLiquidity = new DecisionFactor<StockStats>
	{
		Key = "HasLiquidity",
		Name = "Empresa possui liquidez diária aceitável?",
		Description = "Verifica se a empresa possui volume de negociação adequado para investidores institucionais.",
		Evaluator = s => s.HasLiquidity
	};

	public static DecisionFactor<StockStats> WellRated = new DecisionFactor<StockStats>
	{
		Key = "WellRated",
		Name = "Empresa é bem avaliada ?",
		Description = "Considera a opinião dos investidores como um fator adicional na decisão.",
		Evaluator = s => s.Info.WellRated
	};

	public static DecisionFactor<StockStats> CurrentPriceBelowPortfolioAverage = new DecisionFactor<StockStats>
	{
		Key = "CurrentPriceBelowPortfolioAverage",
		Name = "Preço atual é menor que o preço médio?",
		Description = "Favorece empresas cujo preço atual está abaixo da média, possivelmente indicando uma oportunidade de compra.",
		Evaluator = s => s.CurrentPriceBelowPortfolioAverage
	};

	public static DecisionFactor<StockStats> HasEnoughYearsOfIPO = new DecisionFactor<StockStats>
	{
		Key = "HasEnoughYearsOfIPO",
		Name = "Empresa com IPO há mais de 5 anos?",
		Description = "Considera empresas que estão no mercado público há mais tempo, indicando maturidade.",
		Evaluator = s => s.HasEnoughYearsOfIPO
	};

	public static DecisionFactor<StockStats> BazinCeilingPriceBelowCurrent = new DecisionFactor<StockStats>
	{
		Key = "BazinCeilingPriceBelowCurrent",
		Name = "Preço teto (Bazin) é menor que o preço atual?",
		Description = "Avalia se o preço atual está acima do teto recomendado pelo método Bazin.",
		Evaluator = s => s.BazinCeilingPriceBelowCurrent
	};

	public static DecisionFactor<StockStats> GrahamFairPriceBelowCurrent = new DecisionFactor<StockStats>
	{
		Key = "GrahamFairPriceBelowCurrent",
		Name = "Preço justo (Benjamin Graham) é menor que o preço atual?",
		Description = "Identifica empresas cujos preços estão acima do valor justo estimado pela fórmula de Graham.",
		Evaluator = s => s.GrahamFairPriceBelowCurrent
	};

	public static DecisionFactor<StockStats> DownTrend = new DecisionFactor<StockStats>
	{
		Key = "DownTrend",
		Name = "Empresa está em tendência de baixa?",
		Description = "Verifica se a empresa está apresentando uma tendência de queda no mercado.",
		Evaluator = s => s.DownTrend
	};

	public static IDictionary<string, DecisionFactor<StockStats>> StockDecisionFactors = new Dictionary<string, DecisionFactor<StockStats>>
	{
		{ HasEnoughYearsInMarket.Key, HasEnoughYearsInMarket },
		{ HasNeverPostedLosses.Key, HasNeverPostedLosses },
		{ ProfitableLastQuarters.Key, ProfitableLastQuarters },
		{ PaidAcceptableDividends.Key, PaidAcceptableDividends },
		{ HasAcceptableROE.Key, HasAcceptableROE },
		{ LowDebtToEquity.Key, LowDebtToEquity },
		{ PositiveRevenueCAGR.Key, PositiveRevenueCAGR },
		{ ProfitGrowth.Key, ProfitGrowth },
		{ HasLiquidity.Key, HasLiquidity },
		{ WellRated.Key, WellRated },
		{ CurrentPriceBelowPortfolioAverage.Key, CurrentPriceBelowPortfolioAverage },
		{ HasEnoughYearsOfIPO.Key, HasEnoughYearsOfIPO },
		{ BazinCeilingPriceBelowCurrent.Key, BazinCeilingPriceBelowCurrent },
		{ GrahamFairPriceBelowCurrent.Key, GrahamFairPriceBelowCurrent },
		{ DownTrend.Key, DownTrend }
	};
}
