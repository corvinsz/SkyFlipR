using SkyFlipR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Tests.Models;

public class AuctionTests
{
	[Theory]
	[InlineData(100_000, 120_000, 0.16666666666666666)]
	[InlineData(200_000, 300_000, 0.3333333333333333)]
	public void GetBuyPriceMargin_CalculatesCorrectly(long buyPrice, long otherBuyPrice, double expectedMargin)
	{
		// Arrange
		var cheap = new Auction { BuyPrice = buyPrice };
		var expensive = new Auction { BuyPrice = otherBuyPrice };
		// Act
		double margin = cheap.GetBuyPriceMargin(expensive);
		// Assert
		Assert.Equal(expectedMargin, margin, 2);
	}
}
