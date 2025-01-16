using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.DTOs;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.JSONFiles;

public class Portfolios : BaseJSONFIleRepository<Portfolio, PortfolioDTO>, IPortfolios
{
	IPortifolioCalculator Calculator { get; }
	IPortfolioAssets PortfolioAssets { get; }
	IBonds Bonds { get; }

	public Portfolios(Context context, DataJSONFilesConfig config, IPortifolioCalculator calculator, IPortfolioAssets portfolioAssets, IBonds bonds)
		: base(context, config, "portfolios")
	{
		Calculator = calculator;
		PortfolioAssets = portfolioAssets;
		Bonds = bonds;
	}

	protected override Portfolio FromDTO(PortfolioDTO dto)
	{
		var assets = PortfolioAssets.ByPortifolio(dto.ID);
		var bonds = Bonds.ByPortifolio(dto.ID);
		return Calculator.Calc(dto, dto.Targets, dto.Name, dto.Main, assets, bonds);
	}

	protected override PortfolioDTO FromEntity(Portfolio entity) => new PortfolioDTO(entity, Context);

	protected override IEnumerable<Portfolio> FromDTO(IEnumerable<PortfolioDTO> dtos)
	{
		var portifolios = new List<Portfolio>();
		var assets = PortfolioAssets.All().GroupBy(p => p.PortfolioID).ToDictionary(g => g.Key, g => g);
		var bonds = Bonds.All().Where(b => !b.Expired).GroupBy(p => p.PortfolioID).ToDictionary(g => g.Key, g => g);
		return dtos.Select(d => Calculator.Calc(d, d.Targets, d.Name, d.Main, assets[d.ID], bonds[d.ID]));
	}

	protected override IEnumerable<PortfolioDTO> FromEntity(IEnumerable<Portfolio> entities) => entities.Select(FromEntity);

	public IEnumerable<Portfolio> FromCurrentAccount() => FromDTO(AllDTO().Where(a => a.AccountID == Context.Account.ID));
}