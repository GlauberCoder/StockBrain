using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Portfolios : BaseJSONFIleRepository<Portfolio, PortfolioDTO>, IPortfolios
{
	IPortifolioCalculator Calculator { get; }

	public Portfolios(Context context, DataJSONFilesConfig config, IPortifolioCalculator calculator)
		: base(context, config, "portfolios")
	{
		Calculator = calculator;
	}

	protected override Portfolio FromDTO(PortfolioDTO dto)
	{
		var assets = PortfolioAssets.ByPortifolio(dto.ID);
		var bonds = Bonds.ByPortifolio(dto.ID);
		return Calculator.Calc(dto, dto.AccountID, dto.Targets, dto.Name, dto.Main, assets, bonds);
	}

	protected override PortfolioDTO FromEntity(Portfolio entity) => new PortfolioDTO(entity);

	protected override IEnumerable<Portfolio> FromDTO(IEnumerable<PortfolioDTO> dtos)
	{
		var portifolios = new List<Portfolio>();
		var assets = PortfolioAssets.All().GroupBy(p => p.PortfolioID).ToDictionary(g => g.Key, g => g);
		var bonds = Bonds.All().Where(b => b.Active).GroupBy(p => p.PortfolioID).ToDictionary(g => g.Key, g => g);
		return dtos.Select(d => Calculator.Calc(d, d.AccountID, d.Targets, d.Name, d.Main, assets.ContainsKey(d.ID) ? assets[d.ID] : Enumerable.Empty<PortfolioAsset>(), bonds.ContainsKey(d.ID) ? bonds[d.ID] : Enumerable.Empty<Bond>()));
	}

	protected override IEnumerable<PortfolioDTO> FromEntity(IEnumerable<Portfolio> entities) => entities.Select(FromEntity);

	public IEnumerable<Portfolio> FromCurrentAccount() => FromDTO(AllDTO().Where(a => a.AccountID == Context.Account.ID));
}