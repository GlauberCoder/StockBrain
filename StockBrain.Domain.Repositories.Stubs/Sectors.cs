using StockBrain.Domain.Models;
using StockBrain.Infra.Abstrations;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Stubs;

public class Sectors : ISectors
{
	Context Context { get; }
	public Sectors(Context context)
	{
	}

	public IEnumerable<Sector> All()
	{
		return new List<Sector> { 
			new Sector 
			{ 
				ID = 1,
				Name = "Saúde"
			},
			new Sector
			{
				ID = 100,
				Name = "Tijolo"
			},
		};
	}
}
