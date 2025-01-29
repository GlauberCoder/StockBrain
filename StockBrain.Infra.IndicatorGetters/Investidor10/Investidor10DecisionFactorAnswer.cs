using HtmlAgilityPack;
using StockBrain.Domain.Models;
using StockBrain.Domain.Models.Enums;
using StockBrain.Infra.IndicatorGetters.Abstractions;
using StockBrain.Infra.Repositories.Abstractions;
using StockBrain.Infra.Repositories.JSONFiles;
using StockBrain.Utils;

namespace StockBrain.Infra.IndicatorGetters.Investidor10;

public class Investidor10DecisionFactorAnswer : IDecisionFactorAnswer
{
	IDecisionFactors DecitionFactors { get; }
	IAssetDecisionFactors AssetDecisionFactors { get; }
	IAssets Assets { get; }
	DataJSONFilesConfig FileConfig { get; }

	public Investidor10DecisionFactorAnswer(IDecisionFactors decitionFactors, IAssetDecisionFactors assetDecisionFactors, IAssets assets, DataJSONFilesConfig fileConfig)
	{
		DecitionFactors = decitionFactors;
		AssetDecisionFactors = assetDecisionFactors;
		Assets = assets;
		FileConfig = fileConfig;
	}
	private const string BaseUrl = "https://investidor10.com.br/";


	string GetURI(string ticker, AssetType type)
	{
		var typeUrl = type switch
		{
			AssetType.Acoes => "acoes",
			AssetType.BDR => "bdrs",
			AssetType.FII => "fiis",
			_ => ""
		};
		return $"{BaseUrl}/{typeUrl}/{ticker}/";
	}

	async Task<IDictionary<long, bool>> Get(string ticker, AssetType type, IDictionary<long, string> map, HttpClient client)
	{
		var document = await GetDocument(ticker, type, client);
		var items = new Dictionary<long, bool>();

		foreach (var criteria in map)
			items.Add(criteria.Key, document.GetElementbyId(criteria.Value).Attributes.Contains("checked"));

		return items;
	}
	HttpClient GetClient()
	{
		var client = new HttpClient();
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
		return client;
	}
	IDictionary<long, string> GetMap() => File.ReadAllText(Path.Combine(FileConfig.BasePath, "decisionFactorsInvestidor10Checkilist.json")).Deserialize<Dictionary<long, string>>();
	async Task<HtmlDocument> GetDocument(string ticker, AssetType type, HttpClient client)
	{
		var response = await client.GetAsync(GetURI(ticker, type));
		var html = await response.Content.ReadAsStringAsync();

		var document = new HtmlDocument();
		document.LoadHtml(html);
		return document;
	}
	public async Task<IEnumerable<Asset>> All()
	{
		var map = GetMap();
		var assets = Assets.All();
		var assetDecisionFactor = AssetDecisionFactors.All().Where(f => f.Factor.Strategy == DecisionFactorAnswerStrategy.FromScrap);
		var factors = DecitionFactors.All().Where(f => (f.Type == AssetType.Acoes || f.Type == AssetType.BDR) && f.Strategy == DecisionFactorAnswerStrategy.FromScrap);
		var answers = await Answer(assets.Where(a => (a.Type == AssetType.Acoes || a.Type == AssetType.BDR) && !a.Risk), assetDecisionFactor, factors);

		AssetDecisionFactors.Save(answers);

		return assets;
	}
	async Task<IEnumerable<AssetDecisionFactor>> Answer(IEnumerable<Asset> assets, IEnumerable<AssetDecisionFactor> assetDecisionFactors, IEnumerable<DecisionFactor> factors)
	{
		var map = GetMap();
		var newAnswers = new List<AssetDecisionFactor>();
		using var client = GetClient();
		foreach (var asset in assets)
		{
			var oldAnswers = assetDecisionFactors.Where(a => a.AssetID == asset.ID).ToDictionary(a => a.Factor.ID, a => a);
			var answers = await Get(asset.Ticker, asset.Type, map, client);
			foreach (var factor in factors)
			{
				if (!oldAnswers.TryGetValue(factor.ID, out var assetAnswer))
					assetAnswer = new AssetDecisionFactor
					{
						Answer = null,
						AssetID = asset.ID,
						Factor = factor
					};
				assetAnswer.Answer = answers[factor.ID];
				newAnswers.Add(assetAnswer);
			}

		}
		return newAnswers;
	}
}
