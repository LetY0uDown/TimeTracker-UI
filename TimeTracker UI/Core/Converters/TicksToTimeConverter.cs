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

        bool.TryParse(parameter?.ToString(), out var isFullDateTime);

        var ticks = (long)value;
        var dateTime = new DateTime(ticks);

        if (isFullDateTime == true) {
            return dateTime.ToString();
        }

        return dateTime.TimeOfDay.ToString();
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
