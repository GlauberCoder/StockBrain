using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Domain.Models.Enumerations;

public class BDRDecisionFactors
{
	public static DecisionFactorEvaluator<BDRStats> HasEnoughYearsInMarket = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasEnoughYearsInMarket",
			Name = "Empresa com mais de 10 anos de Bolsa?",
			Description = "Favorece empresas que estão há mais de 10 anos no mercado, indicando estabilidade e histórico sólido."
		},
		Evaluator = s => s.HasEnoughYearsInMarket,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> HasNeverPostedLosses = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasNeverPostedLosses",
			Name = "Empresa nunca deu prejuízo (ano fiscal)?",
			Description = "Favorece empresas que consistentemente geram lucros e não registraram prejuízos."
		},
		Evaluator = s => s.Info.HasNeverPostedLosses,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> ProfitableLastQuarters = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "ProfitableLastQuarters",
			Name = "Empresa com lucro nos últimos trimestres?",
			Description = "Indica empresas que mantêm um histórico consistente de lucro trimestral."
		},
		Evaluator = s => s.Info.ProfitableLastQuarters,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> HasAcceptableROE = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasAcceptableROE",
			Name = "Empresa possui ROE aceitável",
			Description = "Favorece empresas com bom retorno sobre o patrimônio, mostrando eficiência em gerar lucros."
		},
		Evaluator = s => s.HasAcceptableROE,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> WellRated = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "WellRated",
			Name = "Empresa é bem avaliada?",
			Description = "Considera a opinião dos investidores como um fator adicional na decisão."
		},
		Evaluator = s => s.Info.WellRated,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> CurrentPriceBelowPortfolioAverage = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "CurrentPriceBelowPortfolioAverage",
			Name = "Preço atual é menor que o preço médio?",
			Description = "Favorece empresas cujo preço atual está abaixo da média, possivelmente indicando uma oportunidade de compra."
		},
		Evaluator = s => s.CurrentPriceBelowPortfolioAverage,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> HasEnoughYearsOfIPO = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasEnoughYearsOfIPO",
			Name = "Empresa com IPO há mais de 5 anos?",
			Description = "Considera empresas que estão no mercado público há mais tempo, indicando maturidade."
		},
		Evaluator = s => s.HasEnoughYearsOfIPO,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> BazinCeilingPriceBelowCurrent = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "BazinCeilingPriceBelowCurrent",
			Name = "Preço teto (Bazin) é maior que o preço atual?",
			Description = "Avalia se o preço atual está abaixo do teto recomendado pelo método Bazin."
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> GrahamFairPriceBelowCurrent = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "GrahamFairPriceBelowCurrent",
			Name = "Preço justo (Benjamin Graham) é maior que o preço atual?",
			Description = "Identifica empresas cujos preços estão abaixo do valor justo estimado pela fórmula de Graham."
		},
		Evaluator = s => s.GrahamFairPriceAboveCurrent,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> DownTrend = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DownTrend",
			Name = "Empresa está em tendência de baixa?",
			Description = "Verifica se a empresa está apresentando uma tendência de queda no mercado."
		},
		Evaluator = s => s.DownTrend,
		NameParts = c => new List<string>{},
		DescriptionPartsParts = c => new List<string> { }
	};

	public static IDictionary<string, DecisionFactorEvaluator<BDRStats>> DecisionFactors = new Dictionary<string, DecisionFactorEvaluator<BDRStats>>
	{
		{ HasEnoughYearsInMarket.Factor.Key, HasEnoughYearsInMarket },
		{ HasNeverPostedLosses.Factor.Key, HasNeverPostedLosses },
		{ ProfitableLastQuarters.Factor.Key, ProfitableLastQuarters },
		{ HasAcceptableROE.Factor.Key, HasAcceptableROE },
		{ WellRated.Factor.Key, WellRated },
		{ CurrentPriceBelowPortfolioAverage.Factor.Key, CurrentPriceBelowPortfolioAverage },
		{ HasEnoughYearsOfIPO.Factor.Key, HasEnoughYearsOfIPO },
		{ BazinCeilingPriceBelowCurrent.Factor.Key, BazinCeilingPriceBelowCurrent },
		{ GrahamFairPriceBelowCurrent.Factor.Key, GrahamFairPriceBelowCurrent },
		{ DownTrend.Factor.Key, DownTrend }
	};
}
