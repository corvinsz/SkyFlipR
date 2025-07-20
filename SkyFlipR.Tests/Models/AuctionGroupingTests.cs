using SkyFlipR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFlipR.Tests.Models;

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
	[InlineData(10, true)]
	[InlineData(19, true)]
	[InlineData(30, false)]
	[InlineData(100, false)]
	public void WithVaryingProfitMargin_AndFixedItemCountToCompareTo_SetsHasCheapFlipCorrectly(int minProfitMargin, bool hasCheapFlip)
	{
		List<Auction> auctions = GetAuctions();

		// Arrange & Act
		var sut = new AuctionGrouping("Sword", auctions, 1, minProfitMargin);

		Assert.Equal(hasCheapFlip, sut.HasCheapFlip);
	}

	[Theory]
	[InlineData(1, true)]
	[InlineData(2, true)]
	[InlineData(3, false)]
	[InlineData(4, false)]
	public void WithVaryingItemCountToCompareTo_AndFixedMargin_SetsHasCheapFlipCorrectly(int itemCountToComparePrice, bool hasCheapFlip)
	{
		List<Auction> auctions = GetAuctions();

		// Arrange & Act
		var sut = new AuctionGrouping("Sword", auctions, itemCountToComparePrice, 20);

		Assert.Equal(hasCheapFlip, sut.HasCheapFlip);
	}
}
