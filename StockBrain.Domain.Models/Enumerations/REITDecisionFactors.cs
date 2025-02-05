using StockBrain.Domain.Models.AssetInfos;

namespace StockBrain.Domain.Models.Enumerations;

public class REITDecisionFactors
{
	public static DecisionFactorEvaluator<REITStats> DYAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DYAboveThresholdRecent",
			Name = "Distribuição de Dividendos (DY) médio superior ao limite últimos 12 meses?",
			Description = "Favorece FIIs que distribuíram consistentemente dividendos acima do benchmark nos últimos 12 meses."
		},
		Evaluator = s => s.DYAboveThresholdRecent,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> DYAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DYAboveThresholdConsolidated",
			Name = "Distribuição de Dividendos (DY) médio superior ao limite últimos 24 meses?",
			Description = "Favorece FIIs que distribuíram consistentemente dividendos acima do benchmark nos 24 meses."
		},
		Evaluator = s => s.DYAboveThresholdConsolidated,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> PVPBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PVPBellowThreshold",
			Name = "Preço/Valor Patrimonial (P/VP) menor que o limite?",
			Description = "Favorece FIIs cujo preço de mercado está abaixo do benchmark em relação ao seu valor patrimonial."
		},
		Evaluator = s => s.PVPBellowThreshold,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> DailyLiquidityAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "DailyLiquidityAboveThreshold",
			Name = "Empresa possui liquidez diária acima do benchmark?",
			Description = "Verifica se o FIIs tem liquidez suficiente para grandes operações no mercado."
		},
		Evaluator = s => s.DailyLiquidityAboveThreshold,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdRecent",
			Name = "Rentabilidade real maior que o benhmarck nos últimos 5 anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos 5 anos."
		},
		Evaluator = s => s.RealROIAboveThresholdRecent,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> RealROIAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "RealROIAboveThresholdConsolidated",
			Name = "Rentabilidade Real maior que o benchmark nos últimos 10 anos?",
			Description = "Favorece REITs com rentabilidade real acima do benchmark nos últimos 10 anos."
		},
		Evaluator = s => s.RealROIAboveThresholdConsolidated,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdRecent = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdRecent",
			Name = "Rentabilidade maior que o benchmark nos últimos 5 anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos 5 anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdRecent,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> NominalROIAboveThresholdConsolidated = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "NominalROIAboveThresholdConsolidated",
			Name = "Rentabilidade maior que o benchmark nos últimos 10 anos?",
			Description = "Favorece FIIs com retorno médio superior ao benchmark nos últimos 10 anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdConsolidated,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> ManagementFeeBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "ManagementFeeBellowThreshold",
			Name = "Taxa de Administração menor que o benchmark?",
			Description = "Verifica se a taxa de administração do FII é competitiva e não ultrapassa o benchmark."
		},
		Evaluator = s => s.ManagementFeeBellowThreshold,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> VacancyBellowThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "VacancyBellowThreshold",
			Name = "Vacância menor que o benchmark?",
			Description = "Favorece FIIs com baixos índices de vacância, garantindo ocupação estável."
		},
		Evaluator = s => s.VacancyBellowThreshold,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> AssetValueAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "AssetValueAboveThreshold",
			Name = "Patrimônio do Fundo superior ao benchmark",
			Description = "Favorece fundos com patrimônio superior ao benchmark, indicando maior robustez."
		},
		Evaluator = s => s.AssetValueAboveThreshold,
		NameParts = c => new List<string> { },
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
			Name = "Preço teto Bazin é superior ao preço atual? ",
			Description = "Verifica se o preço atual está abaixo do preço teto segundo Bazin."
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent,
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
			Name = "Diversificação acima do benchmark?",
			Description = "Favorece REITs com alta diversificação regional, indicando menor risco."
		},
		Evaluator = s => s.RegionsAboveThreshold,
		NameParts = c => new List<string> { },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<REITStats> PropertyAmountAboveThreshold = new DecisionFactorEvaluator<REITStats>
	{
		Factor = new DecisionFactor
		{
			Key = "PropertyAmountAboveThreshold",
			Name = "Quantidade de imóveis acima do benchmark?",
			Description = "Favorece REITs com uma quantidade imóveis acima do benchmark, garantindo diversificação interna."
		},
		Evaluator = s => s.PropertyAmountAboveThreshold,
		NameParts = c => new List<string> { },
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
