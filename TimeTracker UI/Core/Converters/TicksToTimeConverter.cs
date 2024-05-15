using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.UI.Core.Converters;

internal sealed class TicksToTimeConverter : IValueConverter
{
    /// <summary>
    /// Конвертирует тики в нормальное время
    /// </summary>
    /// <param name="value">Тики</param>
    /// <param name="parameter">Показать полную дату, или только время</param>
    /// <returns></returns>
    public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) {
            return "0:00:00";
        }

        var ticks = (long)value;
        var dateTime = new DateTime(ticks);

        if (bool.TryParse(parameter?.ToString(), out var isFullDateTime) && isFullDateTime) {
            return dateTime.ToString();
        }

        return new TimeSpan(ticks);
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
