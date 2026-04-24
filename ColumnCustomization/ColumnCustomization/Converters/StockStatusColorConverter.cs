using System.Globalization;

namespace ColumnCustomization
{
    public class StockStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status switch
                {
                    "In Stock" => Color.FromArgb("#90EE90"),
                    "Low Stock" => Color.FromArgb("#FFD700"),
                    "Out of Stock" => Color.FromArgb("#FFB6C6"),
                    _ => Colors.White
                };
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
