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
		NameParts = c => new List<string> { c.Config.DividendYieldRecentThreshold.PercentageFormat(), c.Config.DividendYieldRecentAmount.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.DividendYieldRecentThreshold.PercentageFormat(), c.Config.DividendYieldRecentAmount.ToString() }
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
		NameParts = c => new List<string> { c.Config.DividendYieldConsolidatedThreshold.PercentageFormat(), c.Config.DividendYieldConsolidatedAmount.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.DividendYieldConsolidatedThreshold.PercentageFormat(), c.Config.DividendYieldConsolidatedAmount.ToString() }
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
		NameParts = c => new List<string> { c.Config.PVPThreshold.PercentageFormat() },
		DescriptionPartsParts = c => new List<string> { c.Config.PVPThreshold.PercentageFormat() }
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

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdRecent",
			Name = "Rentabilidade real maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdRecent,
		NameParts = c => new List<string> { c.Config.RealROIThresholdRecent.PercentageFormat(), c.Config.RecentROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.RecentROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdConsolidated",
			Name = "Rentabilidade Real maior que {0} nos últimos {1} anos?",
			Description = "Favorece REITs com rentabilidade real acima do benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdConsolidated,
		NameParts = c => new List<string> { c.Config.RealROIThresholdConsolidated.PercentageFormat(), c.Config.ConsolidatedROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.ConsolidatedROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdRecent",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdRecent,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdRecent.PercentageFormat(), c.Config.RecentROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.RecentROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdConsolidated",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdRecent,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdConsolidated.PercentageFormat(), c.Config.ConsolidatedROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.ConsolidatedROIInYears.ToString() }
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
		NameParts = c => new List<string> { c.Config.ManagementFeeThreshold.PercentageFormat() },
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
		NameParts = c => new List<string> { c.Config.VacancyRateThreshold.PercentageFormat() },
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
		DescriptionPartsParts = c => new List<string> { c.Config.BazinExpectedReturn.ToString(), c.Config.BazinYearAmount.ToString() }
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
		{ RealROIAboveThresholdRecent.Factor.Key, RealROIAboveThresholdRecent },
		{ RealROIAboveThresholdConsolidated.Factor.Key, RealROIAboveThresholdConsolidated },
		{ NominalROIAboveThresholdRecent.Factor.Key, NominalROIAboveThresholdRecent },
		{ NominalROIAboveThresholdConsolidated.Factor.Key, NominalROIAboveThresholdConsolidated },
		{ ManagementFeeBellowThreshold.Factor.Key, ManagementFeeBellowThreshold },
		{ VacancyBellowThreshold.Factor.Key, VacancyBellowThreshold },
		{ AssetValueAboveThreshold.Factor.Key, AssetValueAboveThreshold },
		{ DownTrend.Factor.Key, DownTrend },
		{ BazinCeilingPriceAboveCurrent.Factor.Key, BazinCeilingPriceAboveCurrent },
		{ WellRated.Factor.Key, WellRated },
		{ RegionsAboveThreshold.Factor.Key, RegionsAboveThreshold },
		{ PropertyAmountAboveThreshold.Factor.Key, PropertyAmountAboveThreshold }
	};
}
