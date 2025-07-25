﻿using System;
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

namespace SkyFlipR.Shared;

public class MessageBoxDialogOptions()
{
    public static MessageBoxDialogOptions Default { get; } = new();

    public string OkText { get; set; } = "OK";
    public string CancelText { get; set; } = "Cancel";
    public string YesText { get; set; } = "Yes";
    public string NoText { get; set; } = "No";
}

/// <summary>
/// Interaction logic for MessageBoxDialog.xaml
/// </summary>
public partial class MessageBoxDialog : UserControl
{
    public MessageBoxDialog(string title,
                            string message,
                            MessageBoxButton messageBoxButton,
                            MessageBoxDialogOptions? options = null)
    {
        InitializeComponent();
        DataContext = this;
        Title = title;
        Message = message;
        OkVisibility = messageBoxButton == MessageBoxButton.OK ||
                       messageBoxButton == MessageBoxButton.OKCancel ? Visibility.Visible : Visibility.Collapsed;
        YesVisibility = messageBoxButton == MessageBoxButton.YesNo ||
                        messageBoxButton == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
        NoVisibility = YesVisibility;
        CancelVisibility = messageBoxButton == MessageBoxButton.OKCancel ||
                           messageBoxButton == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
        Options = options ?? MessageBoxDialogOptions.Default;
    }

    public string Title { get; }
    public string Message { get; }
    public MessageBoxDialogOptions Options { get; }
    public Visibility OkVisibility { get; }
    public Visibility YesVisibility { get; }
    public Visibility NoVisibility { get; }
    public Visibility CancelVisibility { get; }
}
