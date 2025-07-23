using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using SkyFlipR.Features.ReleaseNotes;
using SkyFlipR.Services;
using SkyFlipR.Services.ErrorHandling;

using Velopack;
using Velopack.Sources;

namespace SkyFlipR.Features.Settings;

public partial class SettingsViewModel : ObservableObject
{
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;
    private readonly IThemeService _themeService;
    private readonly IErrorHandler _errorHandler;

    public SettingsViewModel(ISnackbarMessageQueue snackbarMessageService,
                             IThemeService themeService,
                             IErrorHandler errorHandler,
                             VelopackUpdaterViewModel velopackUpdater)
    {
        _snackbarMessageQueue = snackbarMessageService;
        _themeService = themeService;
        _errorHandler = errorHandler;
        VelopackUpdater = velopackUpdater;
        SelectedTheme = ThemeOptions.First();
    }

    public BaseTheme[] ThemeOptions { get; } = Enum.GetValues<BaseTheme>();
    public VelopackUpdaterViewModel VelopackUpdater { get; }

    [ObservableProperty]
    private BaseTheme _selectedTheme;

    partial void OnSelectedThemeChanged(BaseTheme value)
    {
        _themeService.SetTheme(value);
    }
}