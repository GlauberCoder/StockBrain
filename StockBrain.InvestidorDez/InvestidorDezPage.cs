using HtmlAgilityPack;
using StockBrain.Domain.Models.Enums;
using StockBrain.Utils;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace StockBrain.InvestidorDez;

public class InvestidorDezPage
{
	HtmlDocument Document { get; }
	public AssetType Type { get; }
	public long ID { get; }
	public IDictionary<int, double> Dividends { get; set; }
	public IDictionary<DateOnly, double> Prices { get; set; }
	public InvestidorDezPage(HtmlDocument document, AssetType type)
	{
		Document = document;
		Type = type;
		ID = GetID();
	}
	public string GetText(string selector, bool selectByID) => FindNode(selector, selectByID)?.InnerHtml.Trim() ?? string.Empty;
	public bool GetChekbox(string selector, bool selectByID) => FindNode(selector, selectByID).Attributes.Contains("checked");
	public double GetDouble(string selector, bool selectByID) {
		var text = GetText(selector, selectByID);
		var multiplier = FindMultiplier(text);
		var divisor = FindDivisor(text);
		var number = CleanTextToNumber(text).ToDouble();
		return (number * multiplier) / divisor;
	}
	long GetID() {
		return Type switch
		{
			AssetType.Acoes => GetIDAcao(),
			AssetType.BDR => GetIDBDR(),
			AssetType.FII => 1l,
			_ => 1
		};
	}
	long GetIDBDR()
	{
		var buttonNode = Document.DocumentNode.SelectSingleNode("//button[@id='follow-company-mobile']");
		return long.Parse(buttonNode.GetAttributeValue("data-id", ""));
	}
	long GetIDAcao()
	{
		var nodes = Document.DocumentNode.Descendants("script");
		var id = 1;
		foreach (var node in nodes)
		{
			var scriptInner = node.InnerHtml.Trim();
			if (scriptInner.Contains("var tickerToCompare"))
			{
				var match = Regex.Match(scriptInner, @"'id':\s*'([^']*)'");
				if (match.Success)
					return long.Parse(match.Groups[1].Value);
			}
		}
		return id;
	}
	HtmlNode FindNode(string selector, bool selectByID) => selectByID ? Document.GetElementbyId(selector) : Document.DocumentNode.SelectSingleNode(selector);
	string CleanTextToNumber(string text)
	{
		return text.ToLower().Remove("b").Remove("bilhões").Remove("m").Remove("milhões").Remove("r$").Remove("%").Trim();
	}
	int FindMultiplier(string text)
	{
		return text.ToLower() switch
		{
			string t when t.Contains("b") || t.Contains("bilhões") => 1000000000,
			string t when t.Contains("m") || t.Contains("milhões") => 100000,
			_ => 1
		};
	}
	int FindDivisor(string text) => text.Contains("%") ? 100 : 1;
}
