using StockBrain.Domain.Models;
using System.Linq.Expressions;
using StockBrain.Utils;
using System.Reflection;

namespace StockBrain.InvestidorDez.Mapper;

public class AssetInfoMap<TInfo>
{
	IList<AssetInfoMapItem<TInfo>> Fields { get; } = new List<AssetInfoMapItem<TInfo>>();
	protected void MapCheckbox(Expression<Func<TInfo, bool>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, true, false);
	protected void Map(Expression<Func<TInfo, double>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, false, true);
	protected void Map(Expression<Func<TInfo, string>> acessor, string selector, bool byID) => Map(acessor.GetPropertyInfo(), selector, byID, false, false);
	protected void Map(PropertyInfo propertyInfo, string selector, bool byID, bool fromCheckbox, bool isDouble)
	{
		Fields.Add(new AssetInfoMapItem<TInfo>
		{
			Selector = selector,
			SelectorIsID = byID,
			IsDouble = isDouble,
			FromCheckBox = fromCheckbox,
			Property = propertyInfo
		});
	}
	public void Set(TInfo entity, InvestidorDezPage page)
	{
		foreach (var field in Fields)
			field.SetValue(entity, page);

	}
}
