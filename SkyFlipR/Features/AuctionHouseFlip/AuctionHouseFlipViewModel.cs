using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using SkyFlipR.Models;
using SkyFlipR.Services;
using SkyFlipR.Services.ErrorHandling;

namespace SkyFlipR.Features.AuctionHouseFlip;

public partial class AuctionHouseFlipViewModel : ObservableObject
{
    private readonly ISkyblockAuctionService _auctionService;
    private readonly IErrorHandler _errorHandler;
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;

    public AuctionHouseFlipViewModel(ISkyblockAuctionService auctionService,
                                     IErrorHandler errorHandler,
                                     ISnackbarMessageQueue snackbarMessageQueue)
    {
        _auctionService = auctionService;
        _errorHandler = errorHandler;
        _snackbarMessageQueue = snackbarMessageQueue;
    }

    private List<AuctionGrouping> _groupedItems;

    [ObservableProperty]
    private List<AuctionGrouping> _filteredGroupedItems;

    [ObservableProperty]
    private List<Category> _categories;

    [ObservableProperty]
    private int _minActiveAuctions = 1000;

    partial void OnMinActiveAuctionsChanged(int value)
    {
        ApplyFilter();
    }

    [RelayCommand]
    private async Task FetchAPI()
    {
        try
        {
            var allBINs = await _auctionService.GetBuyItNowAuctionsAsync().ToListAsync();

            Categories = allBINs.Select(x =>
            {
                var cat = new Category(x.Category);
                cat.IsSelectedChanged += OnIsSelectedChanged;
                return cat;
            })
            .DistinctBy(x => x.Name)
            .OrderBy(x => x.Name)
            .ToList();

            _groupedItems = allBINs
                .GroupBy(a => new { a.CleansedItemName, a.Tier })
                .Select(g => new AuctionGrouping($"{g.Key.CleansedItemName} [{g.Key.Tier}]", g.ToList()))
                .OrderByDescending(x => x.ProfitMargin)
                .ToList();

            ApplyFilter();
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
    }

    private void ApplyFilter()
    {
        var selectedCategories = Categories.Where(c => c.IsSelected).Select(c => c.Name).ToHashSet();

        FilteredGroupedItems = _groupedItems.Where(g => selectedCategories.Contains(g.ItemCategory) &&
                                                        g.Auctions.Count >= MinActiveAuctions)
                                            .ToList();
    }

    private void OnIsSelectedChanged(object? sender, bool e)
    {
        ApplyFilter();
    }
}
