using Newtonsoft.Json.Linq;
using StockBrain.Domain.Models;
using System.Reflection;

namespace StockBrain.InvestidorDez.Mapper;

public class AssetInfoMapItem<TInfo>
{
	public bool FromCheckBox { get; init; }
	public bool IsDouble { get; init; }
	public bool SelectorIsID { get; init; }
	public string Selector  { get; init; }
	public PropertyInfo Property  { get; init; }

	public void SetValue(TInfo entity, InvestidorDezPage page)
	{
		if (FromCheckBox) 
			Property.SetValue(entity, page.GetChekbox(Selector, SelectorIsID));

		if (IsDouble)
			Property.SetValue(entity, page.GetDouble(Selector, SelectorIsID));

		if (!FromCheckBox && !IsDouble)
			Property.SetValue(entity, page.GetText(Selector, SelectorIsID));
	}

}
