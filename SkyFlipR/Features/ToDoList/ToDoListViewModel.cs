using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MaterialDesignThemes.Wpf;

using SkyFlipR.Services;
using SkyFlipR.Services.ErrorHandling;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoListViewModel : ObservableObject
{
    private readonly string[] _files = ["daily.json", "weekly.json"];
    private readonly IErrorHandler _errorHandler;
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;
    private readonly IFileHandler _fileHandler;

    public ObservableCollection<ToDoItemGroup> Items { get; } = [];

    public ToDoListViewModel(IErrorHandler errorHandler,
                             ISnackbarMessageQueue snackbarMessageQueue,
                             IFileHandler fileHandler)
    {
        _errorHandler = errorHandler;
        _snackbarMessageQueue = snackbarMessageQueue;
        _fileHandler = fileHandler;
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
            string filePath = Path.Combine(_fileHandler.BaseFolder, file);
            string json = _fileHandler.ReadFile(filePath);
            Items.Add(new ToDoItemGroup(json));
        }
    }
}
