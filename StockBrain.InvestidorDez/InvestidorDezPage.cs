using HtmlAgilityPack;
using StockBrain.Domain.Models.Enums;
using StockBrain.InvestidorDez.Clients;
using StockBrain.Utils;
using System.Text.RegularExpressions;

namespace StockBrain.InvestidorDez;

public class InvestidorDezPage
{
	HtmlDocument Document { get; }
	public AssetType Type { get; }
	public long ID { get; }
	public IDictionary<DateOnly, double> Dividends { get; }
	public IDictionary<DateOnly, double> Prices { get; }
	public IDictionary<DateOnly, double> DividendYields { get; }
	public InvestidorRequester Requester { get; }
	public InvestidorDezPage(HtmlDocument document, string ticker, AssetType type, InvestidorRequester requester)
	{
		Document = document;
		Type = type;
		ID = GetID();
		Prices = requester.GetPrices(ticker, ID).Result;
		Dividends = requester.GetDividends(ticker, ID).Result;
		DividendYields = requester.GetDividendYields(ticker, ID).Result;
		Requester = requester;
	}
	public string GetText(string selector, bool selectByID) => FindNode(selector, selectByID)?.InnerHtml.Trim() ?? string.Empty;
	public bool GetChekbox(string selector, bool selectByID) => FindNode(selector, selectByID).Attributes.Contains("checked");
	public double GetDouble(string selector, bool selectByID) {
		var text = GetText(selector, selectByID);
		if (string.IsNullOrWhiteSpace(text))
			return 0;
		var multiplier = FindMultiplier(text);
		var divisor = FindDivisor(text);
		var number = CleanTextToNumber(text).ToDouble();
		return ((number * multiplier) / divisor).ToPrecision(4);
	}
	long GetID() {
		return Type switch
		{
			AssetType.Acoes => GetIDStock(),
			AssetType.BDR => GetIDBDR(),
			AssetType.FII => GetIDREIT(),
			_ => 1
		};
	}
	long GetIDBDR()
	{
		var buttonNode = Document.DocumentNode.SelectSingleNode("//button[@id='follow-company-mobile']");
		return long.Parse(buttonNode.GetAttributeValue("data-id", ""));
	}
	long GetIDStock() => GetIDREITorStock(@"'id':\s*'([^']*)'");
	long GetIDREIT() => GetIDREITorStock(@"""id""\s*:\s*(\d+)");
	long GetIDREITorStock(string regex)
	{
		var nodes = Document.DocumentNode.Descendants("script");
		var id = 1;
		foreach (var node in nodes)
		{
			var scriptInner = node.InnerHtml.Trim();
			if (scriptInner.Contains("var tickerToCompare"))
			{
				var match = Regex.Match(scriptInner, regex);
				if (match.Success)
					return long.Parse(match.Groups[1].Value);
			}
		}
		return id;
	}
	public HtmlNode FindNode(string selector, bool selectByID) => selectByID ? Document.GetElementbyId(selector) : Document.DocumentNode.SelectSingleNode(selector);
	string CleanTextToNumber(string text)
	{
		var regex = new Regex(@"(?<!\d)([+-]?\d{1,3}(?:\.\d{3})*(?:,\d+)?)(?!\d)");
		var matches = regex.Matches(text.ToLower().Trim());
		if (matches.Count > 0)
			return matches[0].Value.Trim();
		else
			return "0";
	}
	int FindMultiplier(string text)
	{
		return text.ToLower() switch
		{
			string t when t.Contains("b") || t.Contains("bilhões") => 1000000000,
			string t when t.Contains("m") || t.Contains("milhões") => 1000000,
			_ => 1
		};
	}
	int FindDivisor(string text) => text.Contains("%") ? 100 : 1;
}
