using StockBrain.Domain.Models;
using StockBrain.Services.Abstrations;
using System.Globalization;

namespace StockBrain.Services;

public class AssetMovementFromCSVUpdater: IAssetMovementFromCSVUpdater
{
	public IEnumerable<AssetMovement> Update(string text, IEnumerable<AssetMovement> movements)
	{
		var fieldSeparator = ";";
		var partialStockMarker = "F";
		var culture = text.Contains(",") ? new CultureInfo("pt-BR") : new CultureInfo("en-US");
		var lineBreaker = text.Contains(Environment.NewLine) ? Environment.NewLine : "\n";
		if (movements?.Any() ?? false)
		{
			foreach (var line in text.Split(lineBreaker).Where(l => !string.IsNullOrEmpty(l) && l.Contains(fieldSeparator)))
			{
				var parts = line.Split(fieldSeparator);
				var ticker = parts[0].Trim();
				ticker = ticker.EndsWith(partialStockMarker) ? ticker.Remove(ticker.Length - 1) : ticker;
				var movement = movements.FirstOrDefault(m => m.Asset.Ticker == ticker);
				if (movement != null)
				{
					var qtd = int.Parse(parts[1].Trim());
					var totalValue = 0d;
					if (parts.Count() > 3 && !double.TryParse(parts[3].Trim(), NumberStyles.Float, culture, out totalValue))
						if (double.TryParse(parts[2].Trim(), NumberStyles.Float, culture, out double unitValue))
							totalValue = unitValue * qtd;
						else
							continue;

					movement.Quantity = qtd;
					movement.Investment = totalValue;
				}
			}
		}

		return movements;
	}

}
