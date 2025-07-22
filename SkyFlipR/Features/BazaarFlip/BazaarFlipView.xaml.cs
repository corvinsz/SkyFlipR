using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Extensions.DependencyInjection;

using SkyFlipR.Features.AuctionHouseFlip;

namespace SkyFlipR.Features.BazaarFlip;

/// <summary>
/// Interaction logic for BazaarFlipView.xaml
/// </summary>
public partial class BazaarFlipView : UserControl
{
    public BazaarFlipView() : this(App.AppServices.GetRequiredService<BazaarFlipViewModel>())
    {
        InitializeComponent();
    }

    public BazaarFlipView(BazaarFlipViewModel viewModel)
    {
        DataContext = viewModel;
    }
}
