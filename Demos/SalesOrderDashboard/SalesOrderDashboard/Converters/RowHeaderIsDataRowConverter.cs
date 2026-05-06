using System.Globalization;
using SalesOrderDashboard.Model;

namespace SalesOrderDashboard.Converters
{
    // Returns true only when the row's binding context is a data item (OrderInfo)
    public class RowHeaderIsDataRowConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is OrderInfo;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
