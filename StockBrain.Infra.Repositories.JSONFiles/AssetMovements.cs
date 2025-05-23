﻿using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class AssetMovements : BaseJSONFIleRepository<AssetMovement, AssetMovementDTO>, IAssetMovements
{
	IAssets Assets { get; }
	IBrokers Brokers { get; }

	public AssetMovements(Context context, DataJSONFilesConfig config, IAssets assets, IBrokers brokers)
		: base(context, config, "assetMovements")
	{
		Assets = assets;
		Brokers = brokers;
	}


	protected override AssetMovement FromDTO(AssetMovementDTO dto)
	{
		var asset = Assets.All().First(s => s.ID == dto.AssetGUID);
		Broker broker = null;
		if(dto.BrokerGUID.HasValue)
			broker = Brokers.ByID(dto.BrokerGUID.Value);
		return dto.ToEntity(asset, broker, Context);
	}

	protected override AssetMovementDTO FromEntity(AssetMovement entity) => new AssetMovementDTO(entity);

	protected override IEnumerable<AssetMovement> FromDTO(IEnumerable<AssetMovementDTO> dtos)
	{
		var assets = Assets.All().ToDictionary(s => s.ID, s => s);
		var brokers = Brokers.All().ToDictionary(s => s.ID, s => s);

		return dtos.Select(d => d.ToEntity(assets[d.AssetGUID], d.BrokerGUID.HasValue ? brokers[d.BrokerGUID.Value] : null, Context));
	}

	protected override IEnumerable<AssetMovementDTO> FromEntity(IEnumerable<AssetMovement> entities) => entities.Select(FromEntity);

	public IEnumerable<AssetMovement> ByAccount(long accountID) => FromDTO(AllDTO().Where(a => a.AccountID == accountID));
	protected override AssetMovement BeforeCreate(AssetMovement entity)
	{
		entity.AccountID = Context.Account.ID;
		entity.Date = Context.Today;
		return base.BeforeCreate(entity);
	}
}
