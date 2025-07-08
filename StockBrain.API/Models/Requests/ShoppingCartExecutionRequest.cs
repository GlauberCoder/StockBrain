using StockBrain.Domain.Models;
using StockBrain.DTOs;

namespace StockBrain.API.Models.Requests;

public class ShoppingCartExecutionRequest
{
	public IEnumerable<string> PortfolioUUIDs { get; set; }
	public IEnumerable<string>? AssetUUIDs { get; set; } 
	public IEnumerable<string>? BondUUIDs { get; set; }
}
