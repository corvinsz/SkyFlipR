using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using SkyFlipR.Services.ErrorHandling;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoListViewModel : ObservableObject
{
    private readonly string[] _files = ["daily.json", "weekly.json"];
    private readonly IErrorHandler _errorHandler;
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;

    public ObservableCollection<ToDoItemGroup> Items { get; } = [];

    public ToDoListViewModel(IErrorHandler errorHandler,
                             ISnackbarMessageQueue snackbarMessageQueue)
    {
        _errorHandler = errorHandler;
        _snackbarMessageQueue = snackbarMessageQueue;

        try
        {
            LoadFiles();
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
    }

    [RelayCommand]
    private void UncheckAll()
    {
        foreach (var item in Items)
        {
            item.UncheckAll();
        }
    }

    private void LoadFiles()
    {
        Items.Clear();
        foreach (string file in _files)
        {
            Items.Add(ToDoItemGroup.ReadFromJsonFile(file));
        }
    }
}
