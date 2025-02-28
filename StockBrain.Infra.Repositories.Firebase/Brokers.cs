using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class Brokers : BaseFirebaseRepository<Broker, Broker>, IBrokers
{
	public Brokers(Context context, DBClient client)
		: base(context, client, "brokers")
	{
	}

	protected override Broker FromDTO(Broker dto) => dto;

	protected override IEnumerable<Broker> FromDTO(IEnumerable<Broker> dtos) => dtos;

	protected override Broker FromEntity(Broker entity) => entity;

	protected override IEnumerable<Broker> FromEntity(IEnumerable<Broker> entities) => entities;
}
