using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;

namespace SkyFlipR.Services;
public interface IDialogService
{
    Task<object?> Show(object content);
}
internal class DialogService : IDialogService
{
    public async Task<object?> Show(object content)
    {
        return await DialogHost.Show(content);
    }
}