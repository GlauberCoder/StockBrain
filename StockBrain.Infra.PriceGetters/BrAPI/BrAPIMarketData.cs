using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrain.Infra.PriceGetters.BrAPI;

public class BrAPIMarketData
{
    public List<BrAPIResult> Results { get; set; }
    public DateTime RequestedAt { get; set; }
    public string Took { get; set; }
}
