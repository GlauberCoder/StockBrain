using FireSharp.Interfaces;
using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Firebase;

public class Brokers : BaseFirebaseRepository<Broker, Broker>, IBrokers
{
	public Brokers(Context context, IFirebaseClient client)
		: base(context, client, "brokers")
	{
	}

	protected override Broker FromDTO(Broker dto) => dto;

	protected override IEnumerable<Broker> FromDTO(IEnumerable<Broker> dtos) => dtos;

	protected override Broker FromEntity(Broker entity) => entity;

	protected override IEnumerable<Broker> FromEntity(IEnumerable<Broker> entities) => entities;
}
