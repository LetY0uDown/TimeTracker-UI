using System.Globalization;
using System.Windows.Data;

namespace TimeTracker.UI.Core.Converters;

public sealed class LongTitleConverter : IValueConverter
{
    public object? Convert (object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        int maxLenght = int.Parse(parameter.ToString()!);

        var title = value.ToString();

        return title!.Length > maxLenght
                    ? title[0..maxLenght] + ".."
                    : title;
    }

    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}