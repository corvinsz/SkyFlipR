using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SkyFlipR.Extensions;
using SkyFlipR.Models;
using SkyFlipR.Services;
using SkyFlipR.Services.ErrorHandling;

namespace SkyFlipR.Features.BazaarFlip;

public partial class BazaarFlipViewModel : ObservableObject
{
    private readonly ISkyblockBazaarService _skyblockBazaarService;
    private readonly IErrorHandler _errorHandler;

    public BazaarFlipViewModel(ISkyblockBazaarService skyblockBazaarService,
                               IErrorHandler errorHandler)
    {
        _skyblockBazaarService = skyblockBazaarService;
        _errorHandler = errorHandler;
    }

    private List<BazaarItem> _allBazaarItems;

    [ObservableProperty]
    private List<BazaarItem> _bazaarItems;

    [ObservableProperty]
    private string _searchText = "";

    partial void OnSearchTextChanged(string value) => ApplyFilter();

    [RelayCommand]
    private async Task FetchAPI()
    {
        try
        {
            _allBazaarItems = await _skyblockBazaarService.FetchBazaarAsync();
            ApplyFilter();
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
    }

    private void ApplyFilter()
    {
        if(_allBazaarItems.IsNullOrEmpty())
        {
            return;
        }

        if (string.IsNullOrEmpty(SearchText))
        {
            BazaarItems = _allBazaarItems;
            return;
        }

        BazaarItems = _allBazaarItems.Where(x => x.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }
}
