using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SkyFlipR.Models;
public partial class Auction : ObservableObject
{
    // existing properties...
    public string Uuid { get; set; } = default!;
    public string ItemName { get; set; } = default!;
    public long BuyPrice { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string Auctioneer { get; set; } = default!;
    public string Tier { get; set; } = default!;
    public string Category { get; set; } = default!;

    public string CleansedItemName { get; set; } = default!;

    public double GetBuyPriceMarginInPercentage(Auction other)
    {
        return 1 - ((double)BuyPrice / (double)other.BuyPrice);
        //return (((double)other.BuyPrice / (double)BuyPrice) - 1) * 100;
    }

    public double GetBuyPriceMarginInCoins(Auction other) => Math.Abs(BuyPrice - other.BuyPrice);

    [RelayCommand]
    internal void CopyViewAuctionCmd()
    {
        string cmd = $"/viewauction {Uuid}";
        Clipboard.SetText(cmd);
    }
}
