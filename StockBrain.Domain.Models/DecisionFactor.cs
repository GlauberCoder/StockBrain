namespace StockBrain.Domain.Models;

public class DecisionFactor
{
	public required string Key { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }

	public DecisionFactor CompleteName(IEnumerable<string> nameParts, IEnumerable<string> descriptionParts)
	{
		if (!nameParts.Any() && !descriptionParts.Any())
			return this;

		return new DecisionFactor
		{
			Key = Key,
			Name = nameParts.Any() ? string.Format(Name, nameParts.ToArray()) : Name,
			Description = descriptionParts.Any() ? string.Format(Description, descriptionParts.ToArray()) : Name,
		};
	}
}
