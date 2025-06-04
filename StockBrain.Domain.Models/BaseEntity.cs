namespace StockBrain.Domain.Models;

public abstract class BaseEntity
{
	public virtual string? GUID { get; set; }
	public bool IsNew() => string.IsNullOrEmpty(GUID) || GUID == Guid.Empty.ToString();
}
