
using System.IO;
using System.Text.Json;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoItemGroup : ObservableObject
{
    public ToDoItemGroup(string jsonString)
    {
        // TODO
        //JsonSerializer.PopulateObject(this, jsonString);
    }

    public string Name { get; }
    public List<ToDoItem> Items { get; } = [];

    [RelayCommand]
    public void UncheckAll()
    {
        foreach (ToDoItem item in Items)
        {
            item.IsCompleted = false;
        }
    }
}