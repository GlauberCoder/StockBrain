using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Domain.Models.Enumerations;

public class StockDecisionFactors
{
	public static DecisionFactorEvaluator<StockStats> HasEnoughYearsInMarket = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasEnoughYearsInMarket",
			Name = "Empresa com mais de 10 anos de Bolsa?",
			Description = "Favorece empresas que estão há mais de 10 anos no mercado, indicando estabilidade e histórico sólido."
		},
		Evaluator = s => s.HasEnoughYearsInMarket
	};

	public static DecisionFactorEvaluator<StockStats> HasNeverPostedLosses = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasNeverPostedLosses",
			Name = "Empresa nunca deu prejuízo (ano fiscal)?",
			Description = "Favorece empresas que consistentemente geram lucros e não registraram prejuízos."
		},
		Evaluator = s => s.Info.HasNeverPostedLosses
	};

	public static DecisionFactorEvaluator<StockStats> ProfitableLastQuarters = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "ProfitableLastQuarters",
			Name = "Empresa com lucro nos últimos trimestres?",
			Description = "Indica empresas que mantêm um histórico consistente de lucro trimestral."
		},
		Evaluator = s => s.Info.ProfitableLastQuarters
	};

	public static DecisionFactorEvaluator<StockStats> PaidAcceptableDividends = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PaidAcceptableDividends",
			Name = "Empresa pagou dividendos aceitáveis nos últimos anos?",
			Description = "Distribuir dividendos é um indicativo de lucro real, favorece empresas que mantêm uma política de dividendos atrativa ao longo dos últimos anos."
		},
		Evaluator = s => s.Info.PaidAcceptableDividends
	};

	public static DecisionFactorEvaluator<StockStats> HasAcceptableROE = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasAcceptableROE",
			Name = "Empresa possui ROE aceitável",
			Description = "Favorece empresas com bom retorno sobre o patrimônio, mostrando eficiência em gerar lucros."
		},
		Evaluator = s => s.HasAcceptableROE
	};

	public static DecisionFactorEvaluator<StockStats> LowDebtToEquity = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "LowDebtToEquity",
			Name = "Empresa possui dívida menor que o patrimônio?",
			Description = "Indica empresas com menor risco financeiro devido a um endividamento controlado."
		},
		Evaluator = s => s.LowDebtToEquity
	};

	public static DecisionFactorEvaluator<StockStats> PositiveRevenueCAGR = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PositiveRevenueCAGR",
			Name = "Empresa apresentou crescimento de receita?",
			Description = "Verifica empresas com um crescimento sustentável e consistente da receita."
		},
		Evaluator = s => s.PositiveRevenueCAGR
	};

	public static DecisionFactorEvaluator<StockStats> ProfitGrowth = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "ProfitGrowth",
			Name = "Empresa apresentou crescimento de lucros nos últimos 5 anos?",
			Description = "Favorece empresas que demonstraram aumento consistente nos lucros ao longo do tempo."
		},
		Evaluator = s => s.PositiveProfitCAGR
	};

	public static DecisionFactorEvaluator<StockStats> HasLiquidity = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasLiquidity",
			Name = "Empresa possui liquidez diária aceitável?",
			Description = "Verifica se a empresa possui volume de negociação adequado para investidores institucionais."
		},
		Evaluator = s => s.HasLiquidity
	};

	public static DecisionFactorEvaluator<StockStats> WellRated = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "WellRated",
			Name = "Empresa é bem avaliada?",
			Description = "Considera a opinião dos investidores como um fator adicional na decisão."
		},
		Evaluator = s => s.Info.WellRated
	};

	public static DecisionFactorEvaluator<StockStats> CurrentPriceBelowPortfolioAverage = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "CurrentPriceBelowPortfolioAverage",
			Name = "Preço atual é menor que o preço médio?",
			Description = "Favorece empresas cujo preço atual está abaixo da média, possivelmente indicando uma oportunidade de compra."
		},
		Evaluator = s => s.CurrentPriceBelowPortfolioAverage
	};

	public static DecisionFactorEvaluator<StockStats> HasEnoughYearsOfIPO = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasEnoughYearsOfIPO",
			Name = "Empresa com IPO há mais de 5 anos?",
			Description = "Considera empresas que estão no mercado público há mais tempo, indicando maturidade."
		},
		Evaluator = s => s.HasEnoughYearsOfIPO
	};

	public static DecisionFactorEvaluator<StockStats> BazinCeilingPriceAboveCurrent = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "BazinCeilingPriceBelowCurrent",
			Name = "Preço teto (Bazin) é maior que o preço atual?",
			Description = "Avalia se o preço atual está abaixo do teto recomendado pelo método Bazin."
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent
	};

	public static DecisionFactorEvaluator<StockStats> GrahamFairPriceAboveCurrent = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "GrahamFairPriceBelowCurrent",
			Name = "Preço justo (Benjamin Graham) é maior que o preço atual?",
			Description = "Identifica empresas cujos preços estão abaixo do valor justo estimado pela fórmula de Graham."
		},
		Evaluator = s => s.GrahamFairPriceAboveCurrent
	};

	public static DecisionFactorEvaluator<StockStats> DownTrend = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DownTrend",
			Name = "Empresa está em tendência de baixa?",
			Description = "Verifica se a empresa está apresentando uma tendência de queda no mercado."
		},
		Evaluator = s => s.DownTrend
	};


	public static IDictionary<string, DecisionFactorEvaluator<StockStats>> DecisionFactors = new Dictionary<string, DecisionFactorEvaluator<StockStats>>
	{
		{ HasEnoughYearsInMarket.Factor.Key, HasEnoughYearsInMarket },
		{ HasNeverPostedLosses.Factor.Key, HasNeverPostedLosses },
		{ ProfitableLastQuarters.Factor.Key, ProfitableLastQuarters },
		{ PaidAcceptableDividends.Factor.Key, PaidAcceptableDividends },
		{ HasAcceptableROE.Factor.Key, HasAcceptableROE },
		{ LowDebtToEquity.Factor.Key, LowDebtToEquity },
		{ PositiveRevenueCAGR.Factor.Key, PositiveRevenueCAGR },
		{ ProfitGrowth.Factor.Key, ProfitGrowth },
		{ HasLiquidity.Factor.Key, HasLiquidity },
		{ WellRated.Factor.Key, WellRated },
		{ CurrentPriceBelowPortfolioAverage.Factor.Key, CurrentPriceBelowPortfolioAverage },
		{ HasEnoughYearsOfIPO.Factor.Key, HasEnoughYearsOfIPO },
		{ BazinCeilingPriceAboveCurrent.Factor.Key, BazinCeilingPriceAboveCurrent },
		{ GrahamFairPriceAboveCurrent.Factor.Key, GrahamFairPriceAboveCurrent },
		{ DownTrend.Factor.Key, DownTrend }
	};
}
