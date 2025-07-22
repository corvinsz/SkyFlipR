using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace SkyFlipR.Features.AuctionHouseFlip;

public partial class Category : ObservableObject
{
    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; }

    [ObservableProperty]
    private bool _isSelected;

    public event EventHandler<bool> IsSelectedChanged;

    partial void OnIsSelectedChanged(bool value)
    {
        IsSelectedChanged?.Invoke(this, value);
    }
}
