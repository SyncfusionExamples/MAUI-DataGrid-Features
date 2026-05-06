using System.Globalization;

namespace SalesOrderDashboard.Converters
{
    public class PaymentStatusToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var status = value?.ToString();
            return status == "Paid" ? Colors.SeaGreen : Colors.IndianRed;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
