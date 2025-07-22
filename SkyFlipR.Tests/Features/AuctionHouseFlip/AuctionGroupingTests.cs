using SkyFlipR.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Tests.Features.AuctionHouseFlip;

public class AuctionGroupingTests
{
    private static List<Auction> GetAuctions()
    {
        return
        [
            new() { ItemName = "Sword 1", BuyPrice = 100_000 },
            new() { ItemName = "Sword 2", BuyPrice = 120_000 },
            new() { ItemName = "Sword 3", BuyPrice = 120_000 },
            new() { ItemName = "Sword 3", BuyPrice = 300_000 }
        ];
    }

    [Theory]
    [InlineData(true, 10)]
    [InlineData(true, 19)]
    [InlineData(false, 30)]
    [InlineData(false, 100)]
    public void WithVaryingProfitMargin_AndFixedItemCountToCompareTo_SetsHasCheapFlipCorrectly(bool hasCheapFlip, int minProfitMargin)
    {
        List<Auction> auctions = GetAuctions();

        // Arrange & Act
        var sut = new AuctionGrouping("Sword", auctions, minProfitMargin);

        Assert.Equal(hasCheapFlip, sut.HasCheapFlip);
    }
}
