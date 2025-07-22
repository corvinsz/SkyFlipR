using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;

namespace SkyFlipR.Services;
public interface IThemeService
{
    void SetTheme(BaseTheme theme);
}

public class ThemeService : IThemeService
{
    public void SetTheme(BaseTheme newTheme)
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();
        theme.SetBaseTheme(newTheme);
        paletteHelper.SetTheme(theme);
    }
}