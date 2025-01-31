using HtmlAgilityPack;
using StockBrain.Utils;
using System.Text.RegularExpressions;

namespace StockBrain.InvestidorDez;

public class InvestidorDezPage
{
	HtmlDocument Document { get; }
	long ID { get; }
	public IDictionary<int, double> Dividends { get; }
	public IDictionary<DateOnly, double> Prices { get; }
	public InvestidorDezPage(HtmlDocument document, IDictionary<int, double> dividends, IDictionary<DateOnly, double> prices)
	{
		Document = document;
		Dividends = dividends;
		ID = GetID();
		Prices = prices;
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
	long GetID()
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
