using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APITesting.Client.WpfClient.Common;

public sealed class BoolVisibilityConverter : IValueConverter
{
    /// <summary>
    /// When Negated is false, true should become Visibility.Visible, and false should become Visibility.Collapsed.
    /// When Negated is true, false should become Visibility.Visible, and true should become Visibility.Collapsed.
    /// </summary>
    public bool Negated { get; set; }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            if (Negated)
            {
                boolValue = !boolValue;
            }
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        throw new ArgumentException("Value must be a boolean");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Visibility visibilityValue)
        {
            var result = visibilityValue == Visibility.Visible;

            if (Negated)
            {
                result = !result;
            }
            return result;
        }

        throw new ArgumentException("Value must be a Visibility");
    }
}