using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Models;
public class Auction
{
	// existing properties...
	public string Uuid { get; set; } = default!;
	public string ItemName { get; set; } = default!;
	public long BuyPrice { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public string Auctioneer { get; set; } = default!;

	public double GetBuyPriceMargin(Auction other)
	{
		return 1 - ((double)BuyPrice / (double)other.BuyPrice);
		//return (((double)other.BuyPrice / (double)BuyPrice) - 1) * 100;
	}
}
