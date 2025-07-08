using StockBrain.Domain.Models;
using StockBrain.DTOs;

namespace StockBrain.API.Models.Requests;

public class ShoppingCartAssetSaveRequest
{
	public string MovementGUID { get; set; }
	public int Quantity { get; set; } 
	public double Investment { get; set; }
}
