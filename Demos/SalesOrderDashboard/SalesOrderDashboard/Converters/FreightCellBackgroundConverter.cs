using Syncfusion.Maui.DataGrid;
using System.Globalization;

namespace SalesOrderDashboard.Converters
{
    public class FreightCellBackgroundConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DataGridCell cell && cell.DataColumn != null && cell.DataColumn.RowData is Model.OrderInfo row)
            {
                if (cell.DataColumn.ColumnIndex == 7)
                {
                    if (string.Equals(row.PaymentStatus, "Not Paid", StringComparison.OrdinalIgnoreCase))
                        return Color.FromArgb("#FFF1F1");
                }
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
