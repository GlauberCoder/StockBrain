using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Services;
using StockBrain.Services.Abstrations;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetsController : Controller
{
	/// <summary>
	/// Gets or sets the portfolios repository.
	/// </summary>
	IPortfolios Portfolios { get; }
	IAssets Assets { get; }
	IAssetInfoUpdater InfoUpdater { get; }
	IPriceUpdater PriceUpdater { get; }
	IAssetInfos AssetInfos { get; }
	IDecisionFactors DecisionFactors { get; }
	IDecisionFactorAnswerSetter DecisionFactorsSetter { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="PortfolioSummariesController"/> class.
	/// </summary>
	/// <param name="portfolios">The portfolios repository instance.</param>
	public AssetsController(IPortfolios portfolios, IAssets assets, IAssetInfoUpdater infoUpdater, IPriceUpdater priceUpdater, IAssetInfos assetInfos, IDecisionFactors decisionFactors, IDecisionFactorAnswerSetter decisionFactorsSetter)
	{
		Portfolios = portfolios;
		Assets = assets;
		InfoUpdater = infoUpdater;
		PriceUpdater = priceUpdater;
		AssetInfos = assetInfos;
		DecisionFactors = decisionFactors;
		DecisionFactorsSetter = decisionFactorsSetter;
	}

	/// <summary>
	/// Retrieves the details of a specific asset in the portfolio by its ticker symbol.
	/// </summary>
	/// <param name="portfolioUUID">The unique identifier of the portfolio.</param>
	/// <param name="ticker">The ticker symbol of the asset.</param>
	/// <returns>A <see cref="PortfolioAssetDetail"/> with the asset details, or null if not found.</returns>
	[HttpGet("Ticker/{ticker}/Portfolio/{portfolioUUID}")]
	public PortfolioAssetDetail AssetByTicker(string portfolioUUID, string ticker) =>
		Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker);



	[HttpGet("Ticker/{ticker}/Portfolio/{portfolioUUID}/Factors")]
	public IEnumerable<DecisionFactorAnswer> AssetByTickerFactors(string portfolioUUID, string ticker) {
		var asset = Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker)?.Asset;
		var info = AssetInfos.By(asset.Asset.Type, asset.Asset.Ticker);
		var factors = DecisionFactors.All();
		return DecisionFactorsSetter.Get(asset, info, factors);
	}
	[HttpGet("Ticker/{ticker}/Info")]
	public AssetInfo AssetInfoBy(string ticker)
	{
		var asset = Assets.ByID(ticker);
		return AssetInfos.By(asset.Type, asset.Ticker);
	}
	[HttpGet("All/Portfolio/{portfolioUUID}")]
	public IEnumerable<PortfolioAssetDetail> AllAssets(string portfolioUUID) =>
		Portfolios.ByID(portfolioUUID).Assets;

	[HttpPost("Ticker/{ticker}/Update/Info")]
	public async Task<IAssetInfoUpdateStatus> UpdateInfo(string ticker) =>
		await InfoUpdater.Update(ticker);
	[HttpPost("Ticker/{ticker}/Update/Price")]
	public IAssetInfoUpdateStatus UpdatePrice(string ticker) =>
		PriceUpdater.Update(ticker);
	[HttpGet("Get/Tickers")]
	public IEnumerable<string> GetTickers() => Assets.All().Select(a => a.GUID);

}
