using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Bonds : BaseJSONFIleRepository<Bond, BondDTO>, IBonds
{
	IBrokers Brokers { get; }
	IBondIssuers Issuers { get; }
	public Bonds(Context context, DataJSONFilesConfig config, IBrokers brokers, IBondIssuers issuers)
		: base(context, config, "bonds")
	{
		Brokers = brokers;
		Issuers = issuers;
	}


	protected override Bond FromDTO(BondDTO dto)
	{
		var broker = Brokers.All().First(s => s.ID == dto.BrokerID);
		var issuer = Issuers.All().First(s => s.ID == dto.IssuerID);
		return dto.ToBond(broker, issuer, Context);
	}

	protected override BondDTO FromEntity(Bond entity) => new BondDTO(entity);

	protected override IEnumerable<Bond> FromDTO(IEnumerable<BondDTO> dtos)
	{
		var brokers = Brokers.All().ToDictionary(s => s.ID, s => s);
		var issuers = Issuers.All().ToDictionary(s => s.ID, s => s);
		var bonds = new List<Bond>();
		foreach (var dto in dtos)
		{
			bonds.Add(dto.ToBond(brokers[dto.BrokerID], issuers[dto.IssuerID], Context));
		}
		return bonds;
	}

	protected override IEnumerable<BondDTO> FromEntity(IEnumerable<Bond> entities) => entities.Select(FromEntity);

	public IEnumerable<Bond> ByPortifolio(long portifolioID) => FromDTO(AllDTO().Where(a => a.PortifolioID == portifolioID)).Where(b => b.Active);
}
