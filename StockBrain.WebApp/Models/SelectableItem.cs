namespace StockBrain.WebApp.Models;

public class SelectableItem<T>
{
	public SelectableItem(T model, bool selected)
	{
		Model = model;
		Selected = selected;
	}

	public T Model { get; }
	public bool Selected { get; set; }
}
