using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;

namespace SkyFlipR.Services;

public interface IDialogService
{
    void Close(object? identifier);
    void Close(object? identifier, object? parameter);
    bool IsDialogOpen(object? identifier);
    Task<object?> Show(object content);
}

public class DialogService : IDialogService
{
    public async Task<object?> Show(object content)
    {
        return await DialogHost.Show(content);
    }

    public bool IsDialogOpen(object? identifier) => DialogHost.IsDialogOpen(identifier);
    public void Close(object? identifier) => DialogHost.Close(identifier);
    public void Close(object? identifier, object? parameter) => DialogHost.Close(identifier, parameter);
}