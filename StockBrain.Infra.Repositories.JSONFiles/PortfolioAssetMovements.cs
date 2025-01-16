using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class PortfolioAssetMovements : BaseJSONFIleRepository<PortfolioAssetMovement, PortfolioAssetMovementDTO>, IPortfolioAssetMovements
{
	IBrokers Brokers { get; }
	public PortfolioAssetMovements(Context context, DataJSONFilesConfig config, IBrokers brokers)
		: base(context, config, "portfolioAssetMovements")
	{
		Brokers = brokers;
	}


	public IEnumerable<PortfolioAssetMovement> ByPortfolio(long portfolioID) => All().Where(p => p.PortfolioID == portfolioID);

	protected override PortfolioAssetMovement FromDTO(PortfolioAssetMovementDTO dto) => dto.ToEntity(Brokers.ByID(dto.BrokerID));
	protected override IEnumerable<PortfolioAssetMovement> FromDTO(IEnumerable<PortfolioAssetMovementDTO> dtos) 
	{
		var brokers = Brokers.All().ToDictionary(b => b.ID, b => b);
		return dtos.Select(d => d.ToEntity(brokers[d.BrokerID]));
	}
	protected override PortfolioAssetMovementDTO FromEntity(PortfolioAssetMovement entity) => new PortfolioAssetMovementDTO(entity);
	protected override IEnumerable<PortfolioAssetMovementDTO> FromEntity(IEnumerable<PortfolioAssetMovement> entities) => entities.Select(FromEntity);
}
