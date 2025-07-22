using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace SkyFlipR.Features.ToDoList;

public partial class ToDoItem : ObservableObject
{
    public ToDoItem(string name)
    {
        Name = name;
    }
    public string Name { get; }

    [ObservableProperty]
    private bool _isCompleted = false;
}
