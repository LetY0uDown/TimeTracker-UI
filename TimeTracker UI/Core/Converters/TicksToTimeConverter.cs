using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.UI.Core.Converters;

internal sealed class TicksToTimeConverter : IValueConverter
{
    public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) {
            return string.Empty;
        }

        var ticks = (long)value;

        return TimeSpan.FromTicks (ticks).ToString();
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
