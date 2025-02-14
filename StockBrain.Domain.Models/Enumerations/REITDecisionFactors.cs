using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Utils;

namespace StockBrain.Domain.Models.Enumerations;

public class REITDecisionFactors
{
	public static DecisionFactorEvaluator<REITStats> DYAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DYAboveThresholdRecent",
			Name = "Distribuição de Dividendos (DY) médio superior á {0} últimos {1} meses?",
			Description = "Favorece FIIs que distribuíram consistentemente dividendos acima de {0} nos últimos {1} meses."
		},
		Evaluator = s => s.DYAboveThresholdRecent,
		NameParts = c => new List<string> { c.Config.DividendYieldRecentThreshold.PercentageFormat(4), c.Config.DividendYieldRecentAmount.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.DividendYieldRecentThreshold.PercentageFormat(4), c.Config.DividendYieldRecentAmount.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> DYAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DYAboveThresholdConsolidated",
			Name = "Distribuição de Dividendos (DY) médio superior á {0} últimos {1} meses?",
			Description = "Favorece FIIs que distribuíram consistentemente dividendos acima de {0} nos últimos {1} meses."
		},
		Evaluator = s => s.DYAboveThresholdRecent,
		NameParts = c => new List<string> { c.Config.DividendYieldConsolidatedThreshold.PercentageFormat(4), c.Config.DividendYieldConsolidatedAmount.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.DividendYieldConsolidatedThreshold.PercentageFormat(4), c.Config.DividendYieldConsolidatedAmount.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> PVPBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PVPBellowThreshold",
			Name = "Preço/Valor Patrimonial (P/VP) menor que {0} ?",
			Description = "Favorece FIIs cujo preço de mercado está abaixo de {0} do seu valor patrimonial."
		},
		Evaluator = s => s.PVPBellowThreshold,
		NameParts = c => new List<string> { c.Config.PVPThreshold.PercentageFormat(4) },
		DescriptionPartsParts = c => new List<string> { c.Config.PVPThreshold.PercentageFormat(4) }
	};

	public static DecisionFactorEvaluator<REITStats> DailyLiquidityAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DailyLiquidityAboveThreshold",
			Name = "Empresa possui liquidez diária acima de {0} ?",
			Description = "Verifica se o FIIs tem liquidez suficiente para grandes operações no mercado."
		},
		Evaluator = s => s.DailyLiquidityAboveThreshold,
		NameParts = c => new List<string> { c.Config.DailyLiquidityThreshold.MonetaryFormat() },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdNear = new DecisionFactorEvaluator<REITStats>
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
	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdMiddle = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdLong = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdNear = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdMiddle = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdLong = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> ManagementFeeBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "ManagementFeeBellowThreshold",
			Name = "Taxa de Administração menor que {0}?",
			Description = "Verifica se a taxa de administração do FII é competitiva."
		},
		Evaluator = s => s.ManagementFeeBellowThreshold,
		NameParts = c => new List<string> { c.Config.ManagementFeeThreshold.PercentageFormat(4) },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> VacancyBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "VacancyBellowThreshold",
			Name = "Vacância menor que {0} ?",
			Description = "Favorece FIIs com baixos índices de vacância, garantindo ocupação estável."
		},
		Evaluator = s => s.VacancyBellowThreshold,
		NameParts = c => new List<string> { c.Config.VacancyRateThreshold.PercentageFormat(4) },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> AssetValueAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "AssetValueAboveThreshold",
			Name = "Patrimônio do Fundo superior a {0} ?",
			Description = "Favorece fundos com patrimônio superior ao benchmark, indicando maior robustez."
		},
		Evaluator = s => s.AssetValueAboveThreshold,
		NameParts = c => new List<string> { c.Config.AssetValueThreshold.MonetaryFormat() },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> DownTrend = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DownTrend",
			Name = "Tendência de baixa?",
			Description = "Verifica se o FII está atualmente em uma tendência de baixa no mercado."
		},
		Evaluator = s => s.DownTrend,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> BazinCeilingPriceAboveCurrent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "BazinCeilingPriceAboveCurrent",
			Name = "Preço teto Bazin é superior ao preço atual?",
			Description = "Verifica se o preço atual está abaixo do preço teto segundo Bazin. Para a formula deve-se considerar o retorno esperado de {0} e a média de dividendos dos últimos {1} anos"
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent,
		NameParts = c => new List<string> {  },
		DescriptionPartsParts = c => new List<string> { c.Config.BazinExpectedReturn.PercentageFormat(4), c.Config.BazinYearAmount.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> CurrentPriceBelowPortfolioAverage = new DecisionFactorEvaluator<REITStats>
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

	public static DecisionFactorEvaluator<REITStats> WellRated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "WellRated",
			Name = "Empresa é bem avaliada pelos usuários do Investidor10?",
			Description = "Considera a opinião dos investidores como um fator adicional na decisão."
		},
		Evaluator = s => s.Info.WellRated,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> RegionsAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RegionsAboveThreshold",
			Name = "Presente em mais que {0} regiões?",
			Description = "Favorece REITs com alta diversificação regional, indicando menor risco."
		},
		Evaluator = s => s.RegionsAboveThreshold,
		NameParts = c => new List<string> { c.Config.RegionsThreshold.ToString()},
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> PropertyAmountAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PropertyAmountAboveThreshold",
			Name = "Possuí mais que {0} imóveis ?",
			Description = "Favorece REITs com uma quantidade imóveis acima do benchmark, garantindo diversificação interna."
		},
		Evaluator = s => s.PropertyAmountAboveThreshold,
		NameParts = c => new List<string> { c.Config.PropertyThreshold.ToString() },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static IDictionary<string, DecisionFactorEvaluator<REITStats>> DecisionFactors = new Dictionary<string, DecisionFactorEvaluator<REITStats>>
	{
		{ DYAboveThresholdRecent.Factor.Key, DYAboveThresholdRecent },
		{ DYAboveThresholdConsolidated.Factor.Key, DYAboveThresholdConsolidated },
		{ PVPBellowThreshold.Factor.Key, PVPBellowThreshold },
		{ DailyLiquidityAboveThreshold.Factor.Key, DailyLiquidityAboveThreshold },
		{ RealROIAboveThresholdNear.Factor.Key, RealROIAboveThresholdNear },
		{ RealROIAboveThresholdMiddle.Factor.Key, RealROIAboveThresholdMiddle },
		{ RealROIAboveThresholdLong.Factor.Key, RealROIAboveThresholdLong },
		{ NominalROIAboveThresholdNear.Factor.Key, NominalROIAboveThresholdNear },
		{ NominalROIAboveThresholdMiddle.Factor.Key, NominalROIAboveThresholdMiddle },
		{ NominalROIAboveThresholdLong.Factor.Key, NominalROIAboveThresholdLong },
		{ ManagementFeeBellowThreshold.Factor.Key, ManagementFeeBellowThreshold },
		{ VacancyBellowThreshold.Factor.Key, VacancyBellowThreshold },
		{ AssetValueAboveThreshold.Factor.Key, AssetValueAboveThreshold },
		{ DownTrend.Factor.Key, DownTrend },
		{ BazinCeilingPriceAboveCurrent.Factor.Key, BazinCeilingPriceAboveCurrent },
		{ WellRated.Factor.Key, WellRated },
		{ RegionsAboveThreshold.Factor.Key, RegionsAboveThreshold },
		{ CurrentPriceBelowPortfolioAverage.Factor.Key, CurrentPriceBelowPortfolioAverage },
		{ PropertyAmountAboveThreshold.Factor.Key, PropertyAmountAboveThreshold }
	};
}
