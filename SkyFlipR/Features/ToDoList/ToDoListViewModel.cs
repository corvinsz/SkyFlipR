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

using Newtonsoft.Json;

using SkyFlipR.Services;
using SkyFlipR.Services.ErrorHandling;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoListViewModel : ObservableObject
{
    private static readonly string[] _files = ["Daily.json", "Weekly.json"];
    private readonly IErrorHandler _errorHandler;
    private readonly ISnackbarMessageQueue _snackbarMessageQueue;
    private readonly IFileHandler _fileHandler;

    public Dictionary<string, List<ToDoItemGroup>> Items { get; } = [];

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
        foreach (var group in Items)
        {
            foreach(var item in group.Value)
            {
                item.UncheckAll();
            }
        }
    }

    private void LoadFiles()
    {
        Items.Clear();
        foreach (string file in _files)
        {
            string filePath = Path.Combine(_fileHandler.BaseFolder, file);
            EnsureFileIsPresent(filePath);

            string json = _fileHandler.ReadFile(filePath);

            var groups = JsonConvert.DeserializeObject<List<ToDoItemGroup>>(json)!;
            string friendlyName = Path.GetFileNameWithoutExtension(file);
            Items.Add(friendlyName, groups);
        }
    }

    private void EnsureFileIsPresent(string file)
    {
        if (!_fileHandler.Exists(file))
        {
            string fileName = Path.GetFileName(file);
            string localPath = Path.Combine(@"Features\ToDoList\Data", fileName);

            _fileHandler.Copy(localPath, file);
        }
    }
}
