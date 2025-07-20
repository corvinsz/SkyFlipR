using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkyFlipR.Models;
using SkyFlipR.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkyFlipR.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
	private static readonly HttpClient _httpClient = new HttpClient();
	private readonly SkyblockAuctionService _auctionService;

	public MainWindowViewModel()
	{
		_auctionService = new SkyblockAuctionService(_httpClient);
	}

	[ObservableProperty]
	private List<AuctionGrouping> _groupedItems;

	[RelayCommand]
	private async Task TaskLoadItems()
	{
		int skipCount = 2; // Skip two lowest to get “normal” price (i.e. use 3rd price)
		var allBINs = await _auctionService.GetBuyItNowAuctionsAsync().ToListAsync();

		GroupedItems = allBINs
			.GroupBy(a => a.ItemName)
			.Select(g => new AuctionGrouping(g.Key, g.ToList(), skipCount, 20))
			.ToList();
	}
}
