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

using SkyFlipR.Services;

namespace SkyFlipR.Services.ErrorHandling;

/// <summary>
/// Interaction logic for ErrorHandlerView.xaml
/// </summary>
public partial class ErrorHandlerView : UserControl
{
    public ErrorHandlerView(ErrorModel viewModel)
    {
        this.DataContext = viewModel;
        InitializeComponent();
    }
}
