using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrain.Domain.Models;

public class Context
{
	public DateOnly Today => DateOnly.FromDateTime(Now);
	public DateTime Now => DateTime.Now;
	public Account Account { get; set; }
}
