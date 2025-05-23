﻿using StockBrain.Domain.Models.AssetInfos;
using StockBrain.Domain.Models.Enums;

namespace StockBrain.InvestidorDez.Mapper;

public class BDRInfoMap : AssetInfoMap<BDRInfo>
{
	public BDRInfoMap()
	{
		Map(s => s.Price, "//section[@id='cards-ticker']/div[@class='_card cotacao']/div[@class='_card-body']/div/span", false);
		Map(s => s.PL, "//section[@id='cards-ticker']/div[@class='_card val']/div[@class='_card-body']/span", false);
		Map(s => s.PVP, "//section[@id='cards-ticker']/div[@class='_card vp']/div[@class='_card-body']/span", false);
		Map(s => s.Equity, Company(3), false);
		Map(s => s.ROE, Indicator(6), false);
		Map(s => s.LPA, Indicator(15), false);
		Map(s => s.VPA, Indicator(14), false);
		Map(s => s.NominalROINear, ReturnBar(5), false);
		Map(s => s.NominalROIMiddle, ReturnBar(6), false);
		Map(s => s.NominalROILong, ReturnBar(7), false);
		Map(s => s.RealROINear, ReturnBar(12), false);
		Map(s => s.RealROIMiddle, ReturnBar(13), false);
		Map(s => s.RealROILong, ReturnBar(14), false);
		MapCheckbox(s => s.HasNeverPostedLosses, "styled-checkbox-profitable", true);
		MapCheckbox(s => s.ProfitableLastQuarters, "styled-checkbox-profitable5years", true);
		MapCheckbox(s => s.WellRated, "styled-checkbox-rating", true);
	}
	string Indicator(int div) =>  $"//div[@id='table-indicators']/div[{div}]/div[1]/span";
	string Company(int div) => $"//div[@id='table-indicators-company']/div[{div}]/span[2]/div[@class='detail-value']";
	string ReturnBar(int div) => $"//div[@class='return-bar ticker']/div[{div}]/span";
}
