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
		Map(s => s.NominalROINear, ReturnBar(5), false);
		Map(s => s.NominalROIMiddle, ReturnBar(6), false);
		Map(s => s.NominalROILong, ReturnBar(7), false);
		Map(s => s.RealROINear, ReturnBar(12), false);
		Map(s => s.RealROIMiddle, ReturnBar(13), false);
		Map(s => s.RealROILong, ReturnBar(14), false);
		Map(s => s.ManagementFee, Company(9), false);
		Map(s => s.VacancyRate, Company(10), false);
		Map(s => s.AssetValue, Company(14), false);
		Map(s => s.WellRated, "//span[@id='rating-component']/div[@class='content-rating']", false, GetRating);
		Map(s => s.PropertyCount, "//table[@id='properties-index-table']", false, GetPropertyCount);
		Map(s => s.RegionCount, "//table[@id='properties-index-table']", false, GetRegionCount);
	}
	string Cards(string name) =>  $"//section[@id='cards-ticker']/div[@class='_card {name}']/div[@class='_card-body']";
	string Company(int div) => $"//div[@id='table-indicators']/div[{div}]/div[@class='desc']/div[@class='value']/span";
	string ReturnBar(int div) => $"//div[@class='return-bar ticker']/div[{div}]/span";
	object GetRating(HtmlNode node) 
	{
		var value = node.Attributes["x-data"].Value;
		var regex = new Regex(@"rating\s*:\s*(\d+)", RegexOptions.IgnoreCase);
		var match = regex.Match(value);
		return (match.Success ? int.Parse(match.Groups[1].Value) : 0) > 3;
	}
	object GetRegionCount(HtmlNode node)
	{
		return node?.SelectNodes(".//tr").Count ?? 0;
	}
	object GetPropertyCount(HtmlNode node)
	{
		var count = 0;
		if (node != null)
		{
			foreach (var childNode in node.SelectNodes(".//tr/td[2]/span"))
				count += int.Parse(childNode.InnerHtml);
		}
		return count;
	}
}
