using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Models;

public class AuctionGrouping
{
	public AuctionGrouping(string itemName, List<Auction> auctions, int itemCountToComparePrice, int minProfitMargin)
	{
		ItemName = itemName;
		Auctions = auctions.OrderBy(a => a.BuyPrice).ToList();

		/*
		 
		 120
	     400
		 400
		 400
		 
		 */

		Auction? cheapest = auctions.FirstOrDefault();
		Auction? previousAuction = null;
		bool hasCheapFlip = false;

		if (cheapest is not null)
		{
			for (int i = 0; i < auctions.Count; i++)
			{
				Auction auction = auctions[i];

				if (i > itemCountToComparePrice) break;
				if (previousAuction is not null)
				{
					double profitMargin = cheapest.GetBuyPriceMargin(previousAuction);
					if (profitMargin > minProfitMargin)
					{
						hasCheapFlip = true;
					}
				}

				previousAuction = auction;
			}
		}

		HasCheapFlip = hasCheapFlip;

		//if (HasCheapFlip)
		//{
		//	cheapest
		//}
	}

	public string ItemName { get; }
	public bool HasCheapFlip { get; } = false;
	public List<Auction> Auctions { get; }
}
