using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
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

	public Portfolios(Context context, DBClient client, IPortifolioCalculator calculator, IAssets assets, IBrokers brokers, IBondIssuers bondIssuers)
		: base(context, client, "portfolios")
	{
		Calculator = calculator;
		Assets = assets;
		Brokers = brokers;
		BondIssuers = bondIssuers;
	}

	protected override Portfolio FromDTO(PortfolioDTO dto)
	{
		var assets = Assets.AllAsDictionary();
		var brokers = Brokers.AllAsDictionary();
		var issuers = BondIssuers.AllAsDictionary();
		var portfolioAssets = dto.Assets?.Select(a => a.Value.ToEntity(assets[a.Value.Ticker], brokers)) ?? Enumerable.Empty<PortfolioAsset>();
		var portfolioBonds = dto.Bonds?.Select(b => b.Value.ToBond(brokers[b.Value.BrokerGUID], issuers[b.Value.IssuerGUID], Context)) ?? Enumerable.Empty<Bond>();
		return Calculator.Calc(dto, dto.Targets, dto.Name, portfolioAssets, portfolioBonds);
	}

	protected override PortfolioDTO FromEntity(Portfolio entity) => new PortfolioDTO(entity);

	protected override IEnumerable<Portfolio> FromDTO(IEnumerable<PortfolioDTO> dtos)
	{
		var assets = Assets.AllAsDictionary();
		var brokers = Brokers.AllAsDictionary();
		var issuers = BondIssuers.AllAsDictionary();
		return dtos.Select(d => Calculator.Calc(d, d.Targets, d.Name, d.Assets?.Select(a => a.Value.ToEntity(assets[a.Value.Ticker], brokers)) ?? Enumerable.Empty<PortfolioAsset>(), d.Bonds?.Select(b => b.Value.ToBond(brokers[b.Value.BrokerGUID], issuers[b.Value.IssuerGUID], Context)) ?? Enumerable.Empty<Bond>())).ToList();
	}

	protected override IEnumerable<PortfolioDTO> FromEntity(IEnumerable<Portfolio> entities) => entities.Select(FromEntity);

	public Portfolio Main() => ByID(Context.Account.MainPortfolio);
	public IEnumerable<EntityReference> References() => Client.GetShallow().Select(r => new EntityReference(r.GUID, r.Name));
}