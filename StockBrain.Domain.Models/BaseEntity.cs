namespace StockBrain.Domain.Models;

public abstract class BaseEntity
{
	public virtual string GUID { get; set; }
	public virtual long ID { get; set; }
}
