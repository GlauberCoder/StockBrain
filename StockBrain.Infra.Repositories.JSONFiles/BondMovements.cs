using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class BondMovements : BaseJSONFIleRepository<BondMovement, BondMovementDTO>, IBondMovements
{
	IBondIssuers Issuers { get; }
	IBrokers Brokers { get; }
	const long GovID = 1;

	public BondMovements(Context context, DataJSONFilesConfig config, IBondIssuers issuers, IBrokers brokers)
		: base(context, config, "bondMovements")
	{
		Issuers = issuers;
		Brokers = brokers;
	}


	protected override BondMovement FromDTO(BondMovementDTO dto)
	{
		var issuer = Issuers.ByID(dto.IssuerGUID);
		var broker = Brokers.ByID(dto.BrokerGUID);
		return dto.ToEntity(issuer, broker, Context);
	}

	protected override BondMovementDTO FromEntity(BondMovement entity) => new BondMovementDTO(entity, Context);

	protected override IEnumerable<BondMovement> FromDTO(IEnumerable<BondMovementDTO> dtos)
	{
		var issuers = Issuers.All().ToDictionary(s => s.ID, s => s);
		var brokers = Brokers.All().ToDictionary(s => s.ID, s => s);

		return dtos.Select(d => d.ToEntity(issuers[d.Type == BondType.Gov ? GovID : d.IssuerGUID], brokers[d.BrokerGUID], Context));
	}

	protected override IEnumerable<BondMovementDTO> FromEntity(IEnumerable<BondMovement> entities) => entities.Select(FromEntity);

	public IEnumerable<BondMovement> ByAccount(long accountID) => FromDTO(AllDTO().Where(a => a.AccountID == accountID));
	protected override BondMovement BeforeCreate(BondMovement entity)
	{
		entity.AccountID = Context.Account.ID;
		entity.Date = Context.Today;
		return base.BeforeCreate(entity);
	}
	protected override BondMovement BeforeSave(BondMovement entity)
	{
		if (entity.Type == BondType.Gov)
			entity.Issuer = Issuers.ByID(GovID);

		if (entity.Issuer.ID == 0)
			Issuers.Save(entity.Issuer);

		return base.BeforeSave(entity);
	}
}
