using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace SkyFlipR;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider AppServices { get; private set; }

    [STAThread]
    private static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task MainAsync(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        await host.StartAsync().ConfigureAwait(true);

        AppServices = host.Services;

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();

        await host.StopAsync().ConfigureAwait(true);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
            => configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {

            // Services
            services.AddSingleton(_ => Current.Dispatcher);

            services.AddSingleton<ISnackbarMessageQueue>(provider =>
            {
                Dispatcher dispatcher = provider.GetRequiredService<Dispatcher>();
                return new SnackbarMessageQueue(TimeSpan.FromSeconds(3.0), dispatcher);
            });

            services.AddSingleton<Services.IDialogService, Services.DialogService>();
            services.AddSingleton<Services.ErrorHandling.IErrorHandler, Services.ErrorHandling.ErrorHandler>();
            services.AddSingleton<Services.IFileHandler, Services.FileHandler>();
            services.AddSingleton<Services.ISkyblockAuctionService, Services.SkyblockAuctionService>(_ =>
            {
                return new Services.SkyblockAuctionService(new System.Net.Http.HttpClient());
            });


            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            // Features
            services.AddSingleton<Features.AuctionHouseFlip.AuctionHouseFlipView>();
            services.AddSingleton<Features.AuctionHouseFlip.AuctionHouseFlipViewModel>();
            services.AddSingleton<Features.ToDoList.ToDoListView>();
            services.AddSingleton<Features.ToDoList.ToDoListViewModel>();

            services.AddSingleton<WeakReferenceMessenger>();
            services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());


        });
}
