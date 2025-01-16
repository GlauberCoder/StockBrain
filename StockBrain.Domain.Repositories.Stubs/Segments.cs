using StockBrain.Domain.Models;
using StockBrain.Infra.Abstrations;
using StockBrain.Infra.Repositories.Abstractions;

namespace StockBrain.Infra.Repositories.Stubs;

public class Segments : ISegments
{
	Context Context { get; }
	public Segments(Context context)
	{
	}

	public IEnumerable<Segment> All()
	{
		return new List<Segment> { 
			new Segment
			{ 
				ID = 1,
				Name = "Serviços Médicos, Hospitalares, Análises e Diagnósticos"
			},
			new Segment
			{
				ID = 2,
				Name = "Medicamentos"
			},
			new Segment
			{
				ID = 100,
				Name = "Shoppings"
			},
		};
	}
}
