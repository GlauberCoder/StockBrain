using HtmlAgilityPack;
using StockBrain.Domain.Models.AssetInfos;
using System.Text.RegularExpressions;

namespace StockBrain.InvestidorDez.Mapper;

public class REITInfoMap : AssetInfoMap<REITInfo>
{

	public REITInfoMap()
	{
		Map(s => s.Price, $"{Cards("cotacao")}/div/span", false);
		Map(s => s.PVP, $"{Cards("vp")}/span", false);
		Map(s => s.DailyLiquidity, $"{Cards("val")}/span", false);
		Map(s => s.NominalROIRecent, $"//div[@class='return-bar ticker']/div[6]/span", false);
		Map(s => s.NominalROIConsolidated, $"//div[@class='return-bar ticker']/div[7]/span", false);
		Map(s => s.RealROIRecent, $"//div[@class='return-bar ticker']/div[13]/span", false);
		Map(s => s.RealROIConsolidated, $"//div[@class='return-bar ticker']/div[14]/span", false);
		Map(s => s.ManagementFee, Company(9), false);
		Map(s => s.VacancyRate, Company(10), false);
		Map(s => s.AssetValue, Company(14), false);
		Map(s => s.IsWellRated, "//span[@id='rating-component']/div[@class='content-rating']", false, GetRating);
		Map(s => s.PropertyCount, "//table[@id='properties-index-table']", false, GetPropertyCount);
		Map(s => s.RegionCount, "//table[@id='properties-index-table']", false, GetRegionCount);
	}
	string Cards(string name) =>  $"//section[@id='cards-ticker']/div[@class='_card {name}']/div[@class='_card-body']";
	string Company(int div) => $"//div[@id='table-indicators']/div[{div}]/div[@class='desc']/div[@class='value']/span";
	object GetRating(HtmlNode node) 
	{
		var value = node.Attributes["x-data"].Value;
		var regex = new Regex(@"rating\s*:\s*(\d+)", RegexOptions.IgnoreCase);
		var match = regex.Match(value);
		return (match.Success ? int.Parse(match.Groups[1].Value) : 0) > 3;
	}
	object GetRegionCount(HtmlNode node)
	{
		return node.SelectNodes(".//tr").Count;
	}
	object GetPropertyCount(HtmlNode node)
	{
		var count = 0;
		foreach (var childNode in node.SelectNodes(".//tr/td[2]/span"))
			count += int.Parse(childNode.InnerHtml);
		
		return count;
	}
}
