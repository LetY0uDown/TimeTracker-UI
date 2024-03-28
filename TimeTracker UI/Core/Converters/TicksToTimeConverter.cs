using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.UI.Core.Converters;

internal sealed class TicksToTimeConverter : IValueConverter
{
    public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {
        var ticks = (long)value;

        return TimeSpan.FromTicks (ticks).ToString("HH:mm");
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
