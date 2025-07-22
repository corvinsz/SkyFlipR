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

namespace SkyFlipR.Features.AuctionHouseFlip
{
    /// <summary>
    /// Interaction logic for AuctionHouseFlipView.xaml
    /// </summary>
    public partial class AuctionHouseFlipView : UserControl
    {
        public AuctionHouseFlipView() : this(App.AppServices.GetRequiredService<AuctionHouseFlipViewModel>())
        {
            InitializeComponent();
        }

        public AuctionHouseFlipView(AuctionHouseFlipViewModel viewModel)
        {
            DataContext = viewModel;
        }
    }
}
