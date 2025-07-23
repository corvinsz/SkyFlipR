using CommunityToolkit.Mvvm.Messaging;

using MaterialDesignThemes.Wpf;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SkyFlipR.Features.ReleaseNotes;

using System.Net.Http;
using System.Windows;
using System.Windows.Threading;

using Velopack;

namespace SkyFlipR;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider AppServices { get; private set; }
    public static bool IsFirstRun { get; private set; } = false;
    private static readonly HttpClient _httpClient = new();

    [STAThread]
    private static void Main(string[] args)
    {
        // Velopack bootstrap: handles install/update hooks early
        VelopackApp.Build()
            .OnFirstRun(v =>
            {
                IsFirstRun = true;
            })
            .Run();

        // After bootstrap, continue with host + WPF startup
        MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task MainAsync(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        await host.StartAsync().ConfigureAwait(true);

        AppServices = host.Services;

        var app = new App();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();

        await host.StopAsync().ConfigureAwait(true);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, cfg) =>
                cfg.AddUserSecrets(typeof(App).Assembly))
            .ConfigureServices((ctx, services) =>
            {
                services.AddSingleton(_ => Current.Dispatcher);
                services.AddSingleton<ISnackbarMessageQueue>(sp =>
                {
                    return new SnackbarMessageQueue(TimeSpan.FromSeconds(3), sp.GetRequiredService<Dispatcher>());
                });
                services.AddSingleton<Services.IThemeService, Services.ThemeService>();
                services.AddSingleton<Services.IDialogService, Services.DialogService>();
                services.AddSingleton<Services.ErrorHandling.IErrorHandler, Services.ErrorHandling.ErrorHandler>();
                services.AddSingleton<Services.IFileHandler, Services.FileHandler>();
                services.AddSingleton<Services.ISkyblockAuctionService, Services.SkyblockAuctionService>(_ =>
                {
                    return new Services.SkyblockAuctionService(_httpClient);
                });
                services.AddSingleton<Services.ISkyblockBazaarService, Services.SkyblockBazaarService>(_ =>
                {
                    return new Services.SkyblockBazaarService(_httpClient);
                });
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton<Features.ReleaseNotes.VelopackUpdaterViewModel>();
                services.AddSingleton<Features.ReleaseNotes.ReleaseNotesDialog>();

                // register other feature viewmodels / views...
                services.AddSingleton<Features.AuctionHouseFlip.AuctionHouseFlipView>();
                services.AddSingleton<Features.AuctionHouseFlip.AuctionHouseFlipViewModel>();
                services.AddSingleton<Features.BazaarFlip.BazaarFlipView>();
                services.AddSingleton<Features.BazaarFlip.BazaarFlipViewModel>();
                services.AddSingleton<Features.ToDoList.ToDoListView>();
                services.AddSingleton<Features.ToDoList.ToDoListViewModel>();
                services.AddSingleton<Features.Settings.SettingsView>();
                services.AddSingleton<Features.Settings.SettingsViewModel>();

                services.AddSingleton<WeakReferenceMessenger>();
                services.AddSingleton<IMessenger>(prov => prov.GetRequiredService<WeakReferenceMessenger>());
            });
}
