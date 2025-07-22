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

namespace SkyFlipR.Features.ToDoList;

/// <summary>
/// Interaction logic for ToDoListView.xaml
/// </summary>
public partial class ToDoListView : UserControl
{
    public ToDoListView() : this(App.AppServices.GetRequiredService<ToDoListViewModel>())
    {
        InitializeComponent();
    }

    public ToDoListView(ToDoListViewModel viewModel)
    {
        DataContext = viewModel;
    }
}
