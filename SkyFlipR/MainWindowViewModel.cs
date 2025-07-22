using System.Net.Http;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using SkyFlipR.Features.ReleaseNotes;
using SkyFlipR.Models;
using SkyFlipR.Services;

namespace SkyFlipR;

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
            .Select(g => new AuctionGrouping(g.Key, g.ToList(), skipCount))
            .OrderBy(x => x.ProfitMargin)
            .ToList();
    }

    private readonly Lazy<Features.ReleaseNotes.ReleaseNotesDialog> _releaseNotesDialog = new(() => new Features.ReleaseNotes.ReleaseNotesDialog());
    [RelayCommand]
    private async Task OpenReleaseNotes()
    {
        if (DialogHost.IsDialogOpen(null))
        {
            DialogHost.Close(null);
        }
        await DialogHost.Show(_releaseNotesDialog.Value);
    }
}
