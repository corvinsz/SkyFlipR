using System.Net.Http;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using Microsoft.Extensions.DependencyInjection;

using SkyFlipR.Features.ReleaseNotes;
using SkyFlipR.Features.Settings;
using SkyFlipR.Models;
using SkyFlipR.Services;

namespace SkyFlipR;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly Lazy<Features.ReleaseNotes.ReleaseNotesDialog> _releaseNotesDialog = new(() => new Features.ReleaseNotes.ReleaseNotesDialog());
    private readonly IDialogService _dialogService;

    public ISnackbarMessageQueue SnackbarMessageQueue { get; }
    public VelopackUpdaterViewModel VelopackUpdater { get; }

    public MainWindowViewModel(IDialogService dialogService,
                               ISnackbarMessageQueue snackbarMessageQueue,
                               VelopackUpdaterViewModel velopackUpdaterViewModel)
    {
        _dialogService = dialogService;
        SnackbarMessageQueue = snackbarMessageQueue;
        VelopackUpdater = velopackUpdaterViewModel;
    }

    [RelayCommand]
    private async Task OpenReleaseNotes()
    {
        if (_dialogService.IsDialogOpen(null))
        {
            _dialogService.Close(null);
        }
        await _dialogService.Show(_releaseNotesDialog.Value);
    }

    [RelayCommand]
    private async Task OpenSettings()
    {
        if (_dialogService.IsDialogOpen(null))
        {
            _dialogService.Close(null);
        }
        var view = App.AppServices.GetRequiredService<SettingsView>();
        await _dialogService.Show(view);
    }
}
