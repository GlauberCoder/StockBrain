﻿using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Utils;

namespace StockBrain.Domain.Models.Enumerations;

public class StockDecisionFactors
{
	public static DecisionFactorEvaluator<StockStats> HasEnoughYearsInMarket = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "HasEnoughYearsInMarket",
			Name = "Empresa com mais de {0} anos de fundação?",
			Description = "Favorece empresas que estão há mais de {0} anos no mercado, indicando estabilidade e histórico sólido."
		},
		Evaluator = s => s.HasEnoughYearsInMarket,
		NameParts = c => new List<string> { c.Config.AgeThreshold.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.AgeThreshold.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> HasNeverPostedLosses = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "HasNeverPostedLosses",
			Name = "Empresa nunca deu prejuízo (ano fiscal)?",
			Description = "Favorece empresas que consistentemente geram lucros e não registraram prejuízos."
		},
		Evaluator = s => s.Info.HasNeverPostedLosses,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string> {}
	};

	public static DecisionFactorEvaluator<StockStats> PLIsNotTooHigh = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Momento",
			Key = "PLIsNotTooHigh",
			Name = "P/L (preço sobre lucro) não é maior que {0}?",
			Description = "Favorece empresas cujo preço não está exagerado quando comparado ao lucro."
		},
		Evaluator = s => s.PLIsNotTooHigh,
		NameParts = c => new List<string> { c.Config.PLThreshold.ToString() },
		DescriptionPartsParts = c => new List<string> { }
	};
	public static DecisionFactorEvaluator<StockStats> PVPIsNotTooHigh = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Momento",
			Key = "PVPIsNotTooHigh",
			Name = "P/VP (preço sobre valor patrimônial) não é maior que {0}?",
			Description = "Favorece empresas cujo preço não está exagerado quando comparado ao patrimônio."
		},
		Evaluator = s => s.PVPIsNotTooHigh,
		NameParts = c => new List<string> { c.Config.PVPThreshold.ToString() },
		DescriptionPartsParts = c => new List<string> { }
	};
	public static DecisionFactorEvaluator<StockStats> ProfitableLastQuarters = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "ProfitableLastQuarters",
			Name = "Empresa com lucro nos últimos {0} trimestres?",
			Description = "Indica empresas que mantêm um histórico consistente de lucro trimestral."
		},
		Evaluator = s => s.Info.ProfitableLastQuarters,
		NameParts = c => new List<string>{ c.Config.ProfitableTimeInQuarters.ToString() },
		DescriptionPartsParts = c => new List<string> { }
	};

	public static DecisionFactorEvaluator<StockStats> PaidAcceptableDividends = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Retorno",
			Key = "PaidAcceptableDividends",
			Name = "Empresa pagou dividendos acima de {0} nos últimos {1} anos?",
			Description = "Distribuir dividendos é um indicativo de lucro real, favorece empresas que mantêm uma política de dividendos atrativa ao longo dos últimos anos."
		},
		Evaluator = s => s.Info.PaidAcceptableDividends,
		NameParts = c => new List<string>{ c.Config.DividendYieldThreshold.PercentageFormat(2), c.Config.DividendYieldTimeInYears.ToString() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> HasAcceptableROE = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "HasAcceptableROE",
			Name = "Empresa possui ROE acima de {0} ?",
			Description = "Favorece empresas com bom retorno sobre o patrimônio, mostrando eficiência em gerar lucros."
		},
		Evaluator = s => s.HasAcceptableROE,
		NameParts = c => new List<string> { c.Config.ROEThreshold.PercentageFormat(2)},
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> LowDebtToEquity = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "LowDebtToEquity",
			Name = "Empresa possui dívida menor que o patrimônio?",
			Description = "Indica empresas com menor risco financeiro devido a um endividamento controlado."
		},
		Evaluator = s => s.LowDebtToEquity,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> PositiveRevenueCAGR = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "PositiveRevenueCAGR",
			Name = "Empresa apresentou crescimento de receita nos últimos {0} anos?",
			Description = "Verifica empresas com um crescimento sustentável e consistente da receita."
		},
		Evaluator = s => s.PositiveRevenueCAGR,
		NameParts = c => new List<string> { c.Config.RevenueGrowthTimeInYears.ToString() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> ProfitGrowth = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "ProfitGrowth",
			Name = "Empresa apresentou crescimento de lucros nos últimos {0} anos?",
			Description = "Favorece empresas que demonstraram aumento consistente nos lucros ao longo do tempo."
		},
		Evaluator = s => s.PositiveProfitCAGR,
		NameParts = c => new List<string> { c.Config.ProfitGrowthTimeInYears.ToString() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> HasLiquidity = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "HasLiquidity",
			Name = "Empresa possui liquidez diária acima de {0} ?",
			Description = "Verifica se a empresa possui volume de negociação adequado para investidores institucionais."
		},
		Evaluator = s => s.HasLiquidity,
		NameParts = c => new List<string> { c.Config.DailyLiquidityThreshold.MonetaryFormat() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> WellRated = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "WellRated",
			Name = "Empresa é bem avaliada?",
			Description = "Considera a opinião dos investidores como um fator adicional na decisão."
		},
		Evaluator = s => s.Info.WellRated,
		NameParts = c => new List<string> { c.Config.DailyLiquidityThreshold.MonetaryFormat() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> CurrentPriceBelowPortfolioAverage = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Momento",
			Key = "CurrentPriceBelowPortfolioAverage",
			Name = "Preço atual é menor que o preço médio?",
			Description = "Favorece empresas cujo preço atual está abaixo da média, possivelmente indicando uma oportunidade de compra."
		},
		Evaluator = s => s.CurrentPriceBelowPortfolioAverage,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> HasEnoughYearsOfIPO = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Resiliência",
			Key = "HasEnoughYearsOfIPO",
			Name = "Empresa com IPO há mais de {0} anos?",
			Description = "Considera empresas que estão no mercado público há mais tempo, indicando maturidade."
		},
		Evaluator = s => s.HasEnoughYearsOfIPO,
		NameParts = c => new List<string>{ c.Config.IPOTimeThreshold.ToString() },
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> BazinCeilingPriceAboveCurrent = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Retorno",
			Key = "BazinCeilingPriceBelowCurrent",
			Name = "Preço teto (Bazin) é maior que o preço atual?",
			Description = "Avalia se o preço atual está abaixo do teto recomendado pelo método Bazin. Para formula é usado um retorno esperado de {0} e a média dos dividendos é calculada nos últimos {1} anos"
		},
		Evaluator = s => s.BazinCeilingPriceAboveCurrent,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>() { c.Config.BazinExpectedReturn.PercentageFormat(4), c.Config.BazinYearAmount.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> GrahamFairPriceAboveCurrent = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "GrahamFairPriceBelowCurrent",
			Name = "Preço justo (Benjamin Graham) é maior que o preço atual?",
			Description = "Identifica empresas cujos preços estão abaixo do valor justo estimado pela fórmula de Graham."
		},
		Evaluator = s => s.GrahamFairPriceAboveCurrent,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> DownTrend = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Momento",
			Key = "DownTrend",
			Name = "Empresa está em tendência de baixa?",
			Description = "Verifica se a empresa está apresentando uma tendência de queda no mercado."
		},
		Evaluator = s => s.DownTrend,
		NameParts = c => new List<string>(),
		DescriptionPartsParts = c => new List<string>()
	};

	public static DecisionFactorEvaluator<StockStats> RealROIAboveThresholdNear = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "RealROIAboveThresholdNear",
			Name = "Rentabilidade real maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdNear,
		NameParts = c => new List<string> { c.Config.RealROIThresholdNear.PercentageFormat(4), c.Config.NearROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.NearROIInYears.ToString() }
	};
	public static DecisionFactorEvaluator<StockStats> RealROIAboveThresholdMiddle = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "RealROIAboveThresholdMiddle",
			Name = "Rentabilidade real maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs com rentabilidade acima da inflação nos considerando os últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdMiddle,
		NameParts = c => new List<string> { c.Config.RealROIThresholdMiddle.PercentageFormat(4), c.Config.MiddleROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.MiddleROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> RealROIAboveThresholdLong = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "RealROIAboveThresholdLong",
			Name = "Rentabilidade Real maior que {0} nos últimos {1} anos?",
			Description = "Favorece REITs com rentabilidade real acima do benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.RealROIAboveThresholdLong,
		NameParts = c => new List<string> { c.Config.RealROIThresholdLong.PercentageFormat(4), c.Config.LongROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.LongROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> NominalROIAboveThresholdNear = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "NominalROIAboveThresholdNear",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdNear,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdNear.PercentageFormat(4), c.Config.NearROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.NearROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> NominalROIAboveThresholdMiddle = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "NominalROIAboveThresholdMiddle",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdMiddle,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdMiddle.PercentageFormat(4), c.Config.MiddleROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.MiddleROIInYears.ToString() }
	};

	public static DecisionFactorEvaluator<StockStats> NominalROIAboveThresholdLong = new DecisionFactorEvaluator<StockStats>
	{
		Factor = new DecisionFactor
		{
			Group = "Crescimento",
			Key = "NominalROIAboveThresholdLong",
			Name = "Rentabilidade maior que {0} nos últimos {1} anos?",
			Description = "Favorece FIIs que tiveram retorno médio superior ao benchmark nos últimos {0} anos."
		},
		Evaluator = s => s.NominalROIAboveThresholdLong,
		NameParts = c => new List<string> { c.Config.NominalROIThresholdLong.PercentageFormat(4), c.Config.LongROIInYears.ToString() },
		DescriptionPartsParts = c => new List<string> { c.Config.LongROIInYears.ToString() }
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
		{ DownTrend.Factor.Key, DownTrend },
		{ RealROIAboveThresholdNear.Factor.Key, RealROIAboveThresholdNear },
		{ RealROIAboveThresholdMiddle.Factor.Key, RealROIAboveThresholdMiddle },
		{ RealROIAboveThresholdLong.Factor.Key, RealROIAboveThresholdLong },
		{ NominalROIAboveThresholdNear.Factor.Key, NominalROIAboveThresholdNear },
		{ NominalROIAboveThresholdMiddle.Factor.Key, NominalROIAboveThresholdMiddle },
		{ NominalROIAboveThresholdLong.Factor.Key, NominalROIAboveThresholdLong },
		{ PLIsNotTooHigh.Factor.Key, PLIsNotTooHigh },
		{ PVPIsNotTooHigh.Factor.Key, PVPIsNotTooHigh },
	};
}
