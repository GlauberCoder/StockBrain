using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrain.Services.Abstrations;

public interface IAssetInfoUpdateStatus
{
	string Ticker { get; }
	bool Done { get; set; }
	bool HasError { get; set; }
	string ErrorMessage { get; set; }
}
