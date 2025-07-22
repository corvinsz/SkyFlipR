using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Humanizer;

namespace SkyFlipR.Converters;

internal class NumberToMetricConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (int.TryParse(value?.ToString(), out int parsedInt))
        {
            return parsedInt.ToMetric();
        }
        if (double.TryParse(value?.ToString(), out double parsedDouble))
        {
            return parsedDouble.ToMetric();
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
