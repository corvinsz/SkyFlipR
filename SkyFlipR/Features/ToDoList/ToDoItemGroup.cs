
using System.IO;
using System.Text.Json;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoItemGroup : ObservableObject
{
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

    internal static ToDoItemGroup ReadFromJsonFile(string file)
    {
        string json = File.ReadAllText(file);
        return JsonSerializer.Deserialize<ToDoItemGroup>(json) ?? throw new Exception("");
    }
}