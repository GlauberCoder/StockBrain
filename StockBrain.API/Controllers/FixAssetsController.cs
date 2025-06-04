using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.API.Controllers;

/// <summary>
/// API controller that provides endpoints for retrieving fixed income asset metadata, such as bond types and indexes.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FixAssetsController : Controller
{
	/// <summary>
	/// Initializes a new instance of the <see cref="FixAssetsController"/> class.
	/// </summary>
	public FixAssetsController()
	{
	}

	/// <summary>
	/// Retrieves all available bond types for fixed income assets.
	/// </summary>
	/// <returns>An enumerable of bond type names.</returns>
	[HttpGet("Types")]
	public IEnumerable<string> Types() => Enum.GetNames<BondType>();

	/// <summary>
	/// Retrieves all available bond indexes for fixed income assets.
	/// </summary>
	/// <returns>An enumerable of bond index names.</returns>
	[HttpGet("Indexes")]
	public IEnumerable<string> Indexes() => Enum.GetNames<BondIndex>();
}
