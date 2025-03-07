using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;
using StockBrain.Utils;

namespace StockBrain.Infra.Repositories.Firebase;

public class AssetMovements : AccountFirebaseRepository<AssetMovement, AssetMovementDTO>, IAssetMovements
{
	IAssets Assets { get; }
	IBrokers Brokers { get; }

	public AssetMovements(Context context, DBClient client, IAssets assets, IBrokers brokers)
		: base(context, client, "assetMovements", false)
	{
		Assets = assets;
		Brokers = brokers;
	}


	protected override AssetMovement FromDTO(AssetMovementDTO dto)
	{
		var asset = Assets.ByID(dto.AssetGUID);
		Broker broker = null;
		if(dto.BrokerGUID.HasValue())
			broker = Brokers.ByID(dto.BrokerGUID);
		return dto.ToEntity(asset, broker, Context);
	}

	protected override AssetMovementDTO FromEntity(AssetMovement entity) => new AssetMovementDTO(entity);

	protected override IEnumerable<AssetMovement> FromDTO(IEnumerable<AssetMovementDTO> dtos)
	{
		var assets = Assets.All().ToDictionary(s => s.GUID, s => s);
		var brokers = Brokers.All().ToDictionary(s => s.GUID, s => s);

		return dtos.Select(d => d.ToEntity(assets[d.AssetGUID], d.BrokerGUID.HasValue() ? brokers[d.BrokerGUID] : null, Context));
	}

	protected override IEnumerable<AssetMovementDTO> FromEntity(IEnumerable<AssetMovement> entities) => entities.Select(FromEntity);

	protected override AssetMovementDTO BeforeCreateDTO(AssetMovementDTO entity)
	{
		entity.Date = Context.Today;
		entity.BrokerGUID = Context.Account.MainVarBroker;
		entity.GUID = entity.AssetGUID;
		return base.BeforeCreateDTO(entity);
	}
}
