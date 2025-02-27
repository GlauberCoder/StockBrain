using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class PortfolioAssetBrokers : BaseFirebaseRepository<PortfolioAssetBroker, PortfolioAssetBrokerDTO>, IPortfolioAssetBrokers
{
	IBrokers Brokers { get; }

	public PortfolioAssetBrokers(Context context, DataBaseClient client, IBrokers brokers)
		: base(context, client, "portfolioAssetBrokers")
	{
		Brokers = brokers;
	}

	protected override PortfolioAssetBroker FromDTO(PortfolioAssetBrokerDTO dto) => dto.ToEntity(Brokers.ByID(dto.ID));

	protected override PortfolioAssetBrokerDTO FromEntity(PortfolioAssetBroker entity) => new PortfolioAssetBrokerDTO(entity);

	protected override IEnumerable<PortfolioAssetBroker> FromDTO(IEnumerable<PortfolioAssetBrokerDTO> dtos)
	{
		var brokers = Brokers.All().ToDictionary(g => g.ID, g => g);
		return dtos.Select(d => d.ToEntity(brokers[d.BrokerID]));
	}

	protected override IEnumerable<PortfolioAssetBrokerDTO> FromEntity(IEnumerable<PortfolioAssetBroker> entities) => entities.Select(FromEntity);

	public IEnumerable<PortfolioAssetBroker> ByPortfolio(long portfolioID) => FromDTO(AllDTO().Where(a => a.PortfolioID == portfolioID));
}