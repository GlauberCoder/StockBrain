using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase.Services;

namespace StockBrain.Infra.Repositories.Firebase;

public class Accounts : BaseFirebaseRepository<Account, Account>, IAccounts
{
	public Accounts(DataBaseClient client)
		: base(new Context(), client, "accounts")
	{
	}

	public Account Get(string uuid) => All().FirstOrDefault(a => a.GUID == uuid);

	protected override Account FromDTO(Account dto) => dto;

	protected override IEnumerable<Account> FromDTO(IEnumerable<Account> dtos) => dtos;

	protected override Account FromEntity(Account entity) => entity;

	protected override IEnumerable<Account> FromEntity(IEnumerable<Account> entities) => entities;
}
