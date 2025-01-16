using StockBrain.Domain.Models;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Accounts : BaseJSONFIleRepository<Account, Account>, IAccounts
{
	public Accounts(DataJSONFilesConfig config)
		: base(new Context(), config, "accounts")
	{
	}

	public Account Get(string uuid) => All().FirstOrDefault(a => a.GUID == uuid);

	protected override Account FromDTO(Account dto) => dto;

	protected override IEnumerable<Account> FromDTO(IEnumerable<Account> dtos) => dtos;

	protected override Account FromEntity(Account entity) => entity;

	protected override IEnumerable<Account> FromEntity(IEnumerable<Account> entities) => entities;
}
