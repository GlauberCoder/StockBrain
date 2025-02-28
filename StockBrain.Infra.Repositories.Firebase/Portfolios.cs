using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class Portfolios : AccountFirebaseRepository<Portfolio, PortfolioDTO>, IPortfolios
{
	IPortifolioCalculator Calculator { get; }
	IAssets Assets { get; }
	IBrokers Brokers { get; }
	IBondIssuers BondIssuers { get; }
	IDecisionFactorAnswerSetter DecisionFactorSetter { get; }
	IAssetInfos AssetInfos { get; }
	IDecisionFactors DecisionFactors { get; }

	public Portfolios(
		Context context, 
		DBClient client, 
		IPortifolioCalculator calculator, 
		IAssets assets, 
		IBrokers brokers, 
		IBondIssuers bondIssuers,
		IDecisionFactorAnswerSetter decisionFactorSetter,
		IAssetInfos assetInfos,
		IDecisionFactors decisionFactors
		)
		: base(context, client, "portfolios")
	{
		Calculator = calculator;
		Assets = assets;
		Brokers = brokers;
		BondIssuers = bondIssuers;
		DecisionFactorSetter = decisionFactorSetter;
		AssetInfos = assetInfos;
		DecisionFactors = decisionFactors;
	}

	protected override Portfolio FromDTO(PortfolioDTO dto)
	{
		var assets = Assets.AllAsDictionary();
		var brokers = Brokers.AllAsDictionary();
		var issuers = BondIssuers.AllAsDictionary();
		var assetInfos = AssetInfos.All();
		var decitionFactors = DecisionFactors.All();
		return FromDTO(dto, assets, brokers, issuers, assetInfos, decitionFactors);
	}

	protected Portfolio FromDTO(PortfolioDTO dto, IDictionary<string, Asset> assets, IDictionary<string,Broker> brokers, IDictionary<string, BondIssuer> issuers, IDictionary<string, AssetInfo> assetInfos, IDictionary<AssetType, IEnumerable<string>> decitionFactors)
	{
		var portfolioAssets = (dto.Assets?.Select(a => a.Value.ToEntity(assets[a.Value.Ticker], brokers)) ?? Enumerable.Empty<PortfolioAsset>()).ToList();
		var portfolioBonds = dto.Bonds?.Select(b => b.Value.ToBond(brokers[b.Value.BrokerGUID], issuers[b.Value.IssuerGUID], Context)) ?? Enumerable.Empty<Bond>();
		portfolioAssets = DecisionFactorSetter.Set(portfolioAssets, assetInfos, decitionFactors).ToList();
		return Calculator.Calc(dto, dto.Targets, dto.Name, portfolioAssets, portfolioBonds);
	}

	protected override PortfolioDTO FromEntity(Portfolio entity) => new PortfolioDTO(entity);

	protected override IEnumerable<Portfolio> FromDTO(IEnumerable<PortfolioDTO> dtos)
	{
		var assets = Assets.AllAsDictionary();
		var brokers = Brokers.AllAsDictionary();
		var issuers = BondIssuers.AllAsDictionary();
		var assetInfos = AssetInfos.All();
		var decitionFactors = DecisionFactors.All();
		return dtos.Select(d => FromDTO(d, assets,brokers,issuers, assetInfos, decitionFactors)).ToList();
	}

	protected override IEnumerable<PortfolioDTO> FromEntity(IEnumerable<Portfolio> entities) => entities.Select(FromEntity);

	public Portfolio Main() => ByID(Context.Account.MainPortfolio);
	public IEnumerable<EntityReference> References() => AllDTO().Select(r => new EntityReference(r.GUID, r.Name));
}