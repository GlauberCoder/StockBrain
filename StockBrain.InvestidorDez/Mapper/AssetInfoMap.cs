using StockBrain.Domain.Models;
using System.Linq.Expressions;
using StockBrain.Utils;
using System.Reflection;
using HtmlAgilityPack;

namespace StockBrain.InvestidorDez.Mapper;

public class AssetInfoMap<TInfo>
{
	IList<AssetInfoMapItem<TInfo>> Fields { get; } = new List<AssetInfoMapItem<TInfo>>();
	protected void MapCheckbox(Expression<Func<TInfo, bool>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, true, false, null);
	protected void Map(Expression<Func<TInfo, double>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, false, true, null);
	protected void Map(Expression<Func<TInfo, string>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, false, false, null);
	protected void Map(Expression<Func<TInfo, int>> acessor, string selector, bool byID, Func<HtmlNode, object> transformer) => Map(acessor.GetPropertyInfo(), selector, byID, false, false, transformer);
	protected void Map(Expression<Func<TInfo, bool>> acessor, string selector, bool byID, Func<HtmlNode, object> transformer) => Map(acessor.GetPropertyInfo(), selector, byID, false, false, transformer);
	protected void Map(PropertyInfo propertyInfo, string selector, bool byID, bool fromCheckbox, bool isDouble, Func<HtmlNode, object> transformer)
	{
		Fields.Add(new AssetInfoMapItem<TInfo>
		{
			Selector = selector,
			SelectorIsID = byID,
			IsDouble = isDouble,
			FromCheckBox = fromCheckbox,
			Property = propertyInfo,
			Transformer = transformer
		});
	}
	public void Set(TInfo entity, InvestidorDezPage page)
	{
		foreach (var field in Fields)
			field.SetValue(entity, page);

	}
}
