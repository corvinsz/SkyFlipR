using SkyFlipR.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Tests.Features.AuctionHouseFlip;

public class AuctionTests
{
    [Theory]
    [InlineData(100_000, 120_000, 16.66)]
    [InlineData(200_000, 300_000, 33.33)]
    public void GetBuyPriceMarginInPercentage_CalculatesCorrectly(long buyPrice, long otherBuyPrice, double expectedMargin)
    {
        // Arrange
        var cheap = new Auction { BuyPrice = buyPrice };
        var expensive = new Auction { BuyPrice = otherBuyPrice };
        // Act
        double margin = cheap.GetBuyPriceMarginInPercentage(expensive);
        // Assert
        Assert.Equal(expectedMargin, margin, 2);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(100_000, 120_000, 20_000)]
    [InlineData(120_000, 100_000, 20_000)]
    [InlineData(120, 125, 5)]
    public void GetBuyPriceMarginInCoins_CalculatesCorrectly(long buyPrice, long otherBuyPrice, double expectedMargin)
    {
        // Arrange
        var cheap = new Auction { BuyPrice = buyPrice };
        var expensive = new Auction { BuyPrice = otherBuyPrice };
        // Act
        double margin = cheap.GetBuyPriceMarginInCoins(expensive);
        // Assert
        Assert.Equal(expectedMargin, margin, 0);
    }
}
