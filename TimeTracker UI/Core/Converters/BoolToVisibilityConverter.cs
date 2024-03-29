using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TimeTracker.UI.Core.Converters;

internal sealed class BoolToVisibilityConverter : IValueConverter
{
    /// <param name="value">Исходное значение</param>
    /// <param name="parameter">При каком значении должен вернуть Visible</param>
    /// <returns></returns>
    public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {
        var b = (bool)value;
        var param = bool.Parse(parameter.ToString()!);

        // Если исходное значение и параметр одинаковые - true иначе false
        var result = !(b ^ param);

        return result ? Visibility.Visible : Visibility.Hidden; 
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}