using System.Windows.Controls;

using Microsoft.Extensions.DependencyInjection;

namespace SkyFlipR.Features.Settings;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
    public SettingsView() : this(App.AppServices.GetRequiredService<SettingsViewModel>())
    {
        InitializeComponent();
    }

    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
