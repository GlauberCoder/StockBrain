using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrain.Infra.PriceGetters.BrAPI;

public class BrAPIResult
{
    public string Currency { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public double RegularMarketChange { get; set; }
    public double RegularMarketChangePercent { get; set; }
    public DateTime RegularMarketTime { get; set; }
    public double RegularMarketPrice { get; set; }
    public double RegularMarketDayHigh { get; set; }
    public string RegularMarketDayRange { get; set; }
    public double RegularMarketDayLow { get; set; }
    public long RegularMarketVolume { get; set; }
    public double RegularMarketPreviousClose { get; set; }
    public double RegularMarketOpen { get; set; }
    public string FiftyTwoWeekRange { get; set; }
    public double FiftyTwoWeekLow { get; set; }
    public double FiftyTwoWeekHigh { get; set; }
    public string Symbol { get; set; }
    public double? PriceEarnings { get; set; }
    public double? EarningsPerShare { get; set; }
    public string Logourl { get; set; }

}
