using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class BondMovements : AccountFirebaseRepository<BondMovement, BondMovementDTO>, IBondMovements
{
	IBondIssuers Issuers { get; }
	IBrokers Brokers { get; }
	const string GovUUID = "GOV";

	public BondMovements(Context context, DBClient client,IBondIssuers issuers, IBrokers brokers)
		: base(context, client, "bondMovements", false)
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
		var issuers = Issuers.All().ToDictionary(s => s.GUID, s => s);
		var brokers = Brokers.All().ToDictionary(s => s.GUID, s => s);

		return dtos.Select(d => d.ToEntity(issuers[d.Type == BondType.Gov ? GovUUID : d.IssuerGUID], brokers[d.BrokerGUID], Context));
	}

	protected override IEnumerable<BondMovementDTO> FromEntity(IEnumerable<BondMovement> entities) => entities.Select(FromEntity);
	protected override BondMovement BeforeCreate(BondMovement entity)
	{
		entity.Date = Context.Today;
		return base.BeforeCreate(entity);
	}
	protected override BondMovement BeforeSave(BondMovement entity)
	{
		if (entity.Type == BondType.Gov)
			entity.Issuer = Issuers.ByID(GovUUID);
		else if (entity.Issuer.IsNew())
			entity.Issuer = Issuers.Save(entity.Issuer);

		return base.BeforeSave(entity);
	}

	public void Add(BondMovementDTO bondMovement)
	{
		Save(FromDTO(bondMovement));
	}
	public void Clear()
	{
		Delete(All());
	}

	public void DefineBroker(string brokerUUID, IEnumerable<string> uuids)
	{
		var broker = Brokers.ByID(brokerUUID);
		foreach (var uuid in uuids)
		{
			var movement = ByID(uuid);
			movement.Broker = broker;
			Save(movement);
		}
	}
}
