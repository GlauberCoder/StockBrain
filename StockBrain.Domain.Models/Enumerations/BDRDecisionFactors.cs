using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Utils;

namespace StockBrain.Domain.Models.Enumerations;

public class BDRDecisionFactors
{
	public static DecisionFactorEvaluator<BDRStats> HasEnoughYearsInMarket = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasEnoughYearsInMarket",
			Name = "Empresa com mais de {0} anos de fundação?",
			Description = "Favorece empresas que estão há mais de {0} anos no mercado, indicando estabilidade e histórico sólido."
		},
		Evaluator = s => s.HasEnoughYearsInMarket,
		NameParts = c => new List<string> { c.Config.AgeThreshold.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.AgeThreshold.ToString() }
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
			Name = "Empresa com lucro nos últimos {0} trimestres?",
			Description = "Indica empresas que mantêm um histórico consistente de lucro trimestral."
		},
		Evaluator = s => s.Info.ProfitableLastQuarters,
		NameParts = c => new List<string> { c.Config.ProfitableTimeInQuarters.ToString() },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<BDRStats> HasAcceptableROE = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "HasAcceptableROE",
			Name = "Empresa possui ROE acima de {0} ?",
			Description = "Favorece empresas com bom retorno sobre o patrimônio, mostrando eficiência em gerar lucros."
		},
		Evaluator = s => s.HasAcceptableROE,
		NameParts = c => new List<string> { c.Config.ROEThreshold.PercentageFormat(2) },
		DescriptionPartsParts = c => new List<string>()
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
			Name = "Empresa com IPO há mais de {0} anos?",
			Description = "Considera empresas que estão no mercado público há mais tempo, indicando maturidade."
		},
		Evaluator = s => s.HasEnoughYearsOfIPO,
		NameParts = c => new List<string> { c.Config.IPOTimeThreshold.ToString() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<BDRStats> BazinCeilingPriceBelowCurrent = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "BazinCeilingPriceBelowCurrent",
			Name = "Preço teto (Bazin) é maior que o preço atual?",
			Description = "Avalia se o preço atual está abaixo do teto recomendado pelo método Bazin. Para formula é usado um retorno esperado de {0} e a média dos dividendos é calculada nos últimos {1} anos"
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>() { c.Config.BazinExpectedReturn.PercentageFormat(4), c.Config.BazinYearAmount.ToString() }
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

	public static DecisionFactorEvaluator<BDRStats> RealROIAboveThresholdNear = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdNear",
			Name = "Rentabilidade real maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdNear,
		NameParts = c => new List<string> { c.Config.RealROIThresholdNear.PercentageFormat(4), c.Config.NearROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.NearROIInYears.ToString() }
	};
	public static DecisionFactorEvaluator<BDRStats> RealROIAboveThresholdMiddle = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdMiddle",
			Name = "Rentabilidade real maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdMiddle,
		NameParts = c => new List<string> { c.Config.RealROIThresholdMiddle.PercentageFormat(4), c.Config.MiddleROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.MiddleROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<BDRStats> RealROIAboveThresholdLong = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdLong",
			Name = "Rentabilidade Real maior que {0} nos últimos {1} anos?",
			Description = "Favorece REITs com rentabilidade real acima do benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdLong,
		NameParts = c => new List<string> { c.Config.RealROIThresholdLong.PercentageFormat(4), c.Config.LongROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.LongROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<BDRStats> NominalROIAboveThresholdNear = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdNear",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdNear,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdNear.PercentageFormat(4), c.Config.NearROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.NearROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<BDRStats> NominalROIAboveThresholdMiddle = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdMiddle",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdMiddle,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdMiddle.PercentageFormat(4), c.Config.MiddleROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.MiddleROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<BDRStats> NominalROIAboveThresholdLong = new DecisionFactorEvaluator<BDRStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdLong",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdLong,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdLong.PercentageFormat(4), c.Config.LongROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.LongROIInYears.ToString() }
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
		{ DownTrend.Factor.Key, DownTrend },
		{ RealROIAboveThresholdNear.Factor.Key, RealROIAboveThresholdNear },
		{ RealROIAboveThresholdMiddle.Factor.Key, RealROIAboveThresholdMiddle },
		{ RealROIAboveThresholdLong.Factor.Key, RealROIAboveThresholdLong },
		{ NominalROIAboveThresholdNear.Factor.Key, NominalROIAboveThresholdNear },
		{ NominalROIAboveThresholdMiddle.Factor.Key, NominalROIAboveThresholdMiddle },
		{ NominalROIAboveThresholdLong.Factor.Key, NominalROIAboveThresholdLong }
	};
}
