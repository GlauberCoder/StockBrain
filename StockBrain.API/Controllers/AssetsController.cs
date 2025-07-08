using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrain.Domain.Abstractions;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.EvaluationConfigs;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.Firebase;
using StockBrain.Services;
using StockBrain.Services.Abstrations;

namespace StockBrain.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AssetsController : Controller
{
	/// <summary>
	/// Gets or sets the portfolios repository.
	/// </summary>
	IPortfolios Portfolios { get; }
	IAssets Assets { get; }
	IAssetInfoUpdater InfoUpdater { get; }
	IPortfolioAssetUpdater PortfolioAssetUpdater { get; }
	IPriceUpdater PriceUpdater { get; }
	IAssetInfos AssetInfos { get; }
	IDecisionFactors DecisionFactors { get; }
	IDecisionFactorAnswerSetter DecisionFactorsSetter { get; }
	StockEvaluationConfig StockEvaluationConfig { get; }
	BDREvaluationConfig BDREvaluationConfig { get; }
	REITEvaluationConfig REITEvaluationConfig { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="PortfolioSummariesController"/> class.
	/// </summary>
	/// <param name="portfolios">The portfolios repository instance.</param>
	public AssetsController(
		IPortfolios portfolios, 
		IAssets assets, 
		IAssetInfoUpdater infoUpdater,
		IPortfolioAssetUpdater portfolioAssetUpdater,
		IPriceUpdater priceUpdater, 
		IAssetInfos assetInfos, 
		IDecisionFactors decisionFactors, 
		IDecisionFactorAnswerSetter decisionFactorsSetter,
		StockEvaluationConfig stockEvaluationConfig,
		BDREvaluationConfig bdrEvaluationConfig,
		REITEvaluationConfig reitEvaluationConfig
		)
	{
		Portfolios = portfolios;
		Assets = assets;
		InfoUpdater = infoUpdater;
		PortfolioAssetUpdater = portfolioAssetUpdater;
		PriceUpdater = priceUpdater;
		AssetInfos = assetInfos;
		DecisionFactors = decisionFactors;
		DecisionFactorsSetter = decisionFactorsSetter;
		StockEvaluationConfig = stockEvaluationConfig;
		BDREvaluationConfig = bdrEvaluationConfig;
		REITEvaluationConfig = reitEvaluationConfig;
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
	[HttpGet("Stock/Ticker/{ticker}/Portfolio/{portfolioUUID}/Stats")]
	public StockStats StockAssetByTickerStats(string portfolioUUID, string ticker)
	{
		var asset = Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker)?.Asset;
		var info = AssetInfos.By(asset.Asset.Type, asset.Asset.Ticker);
		return new StockStats(asset, (StockInfo)info, StockEvaluationConfig);
	}
	[HttpGet("BDR/Ticker/{ticker}/Portfolio/{portfolioUUID}/Stats")]
	public BDRStats BDRAssetByTickerStats(string portfolioUUID, string ticker)
	{
		var asset = Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker)?.Asset;
		var info = AssetInfos.By(asset.Asset.Type, asset.Asset.Ticker);
		return new BDRStats(asset, (BDRInfo)info, BDREvaluationConfig);
	}
	[HttpGet("REIT/Ticker/{ticker}/Portfolio/{portfolioUUID}/Stats")]
	public REITStats REITAssetByTickerStats(string portfolioUUID, string ticker)
	{
		var asset = Portfolios.ByID(portfolioUUID).Assets.FirstOrDefault(a => a.Asset.Asset.Ticker == ticker)?.Asset;
		var info = AssetInfos.By(asset.Asset.Type, asset.Asset.Ticker);
		return new REITStats(asset, (REITInfo)info, REITEvaluationConfig);
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
	[HttpGet("Tickers")]
	public IEnumerable<string> GetTickers() => Assets.All().Select(a => a.GUID);
	[HttpPost("Update/Quantity/Portfolio/{portfolioUUID}")]
	public void UpdateQuantity(string portfolioUUID, [FromBody]IDictionary<string,int> newQuantities) =>
		PortfolioAssetUpdater.UpdateQuantities(portfolioUUID, newQuantities);
	[HttpGet("Quantity/Portfolio/{portfolioUUID}")]
	public IDictionary<string, int> PortfolioQuantity(string portfolioUUID) 
		=> Portfolios.ByID(portfolioUUID).Assets.ToDictionary(a => a.Asset.Asset.Ticker, a => a.Asset.Quantity);

}
