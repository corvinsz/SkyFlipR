using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Xaml.Behaviors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Models;

public partial class AuctionGrouping : ObservableObject
{
    public AuctionGrouping(string itemName, List<Auction> auctions, double minProfitMargin = 0)
    {
        ItemName = itemName;
        Auctions = auctions.OrderBy(a => a.BuyPrice).ToList();
        ItemCategory = Auctions.FirstOrDefault()?.Category ?? "UNKNOWN";
        FirstFiveAuctions = Auctions.Take(5).ToList();
        /*

		 120
	     400
		 400
		 400

		 */

        if (Auctions.Count >= 2)
        {
            Auction? cheapestItem = Auctions.ElementAt(0);
            Auction? secondItem = Auctions.ElementAt(1);

            ProfitMargin = cheapestItem.GetBuyPriceMarginInCoins(secondItem);

            if (ProfitMargin >= minProfitMargin)
            {
                HasCheapFlip = true;
            }
        }

        //bool hasCheapFlip = false;

        //if (cheapest is not null)
        //{
        //    for (int i = 0; i < auctions.Count; i++)
        //    {
        //        Auction auction = auctions[i];

        //        if (i > itemCountToComparePrice) break;
        //        if (previousAuction is not null)
        //        {
        //            double profitMargin = cheapest.GetBuyPriceMargin(previousAuction);
        //            if (profitMargin > minProfitMargin)
        //            {
        //                hasCheapFlip = true;
        //            }
        //        }

        //        previousAuction = auction;
        //    }
        //}

        //HasCheapFlip = hasCheapFlip;

        //if (HasCheapFlip)
        //{
        //	cheapest
        //}
    }

    public string ItemName { get; }
    public string ItemCategory { get; }
    public double ProfitMargin { get; }
    public bool HasCheapFlip { get; } = false;
    public List<Auction> Auctions { get; }
    public IReadOnlyList<Auction> FirstFiveAuctions { get; }

    [RelayCommand]
    private void CopyCheapestViewAuctionCmd()
    {
        if (Auctions.Count > 0)
        {
            Auctions.First().CopyViewAuctionCmd();
        }
    }
}
