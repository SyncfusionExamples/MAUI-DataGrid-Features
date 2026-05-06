using Syncfusion.Maui.DataGrid;
using System.Globalization;

namespace SalesOrderDashboard.Converters
{
    public class ForeColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo info)
        {
            if (value is not DataGridCell gridCell || gridCell.DataColumn == null)
                return null;

            int columnIndex = gridCell.DataColumn.ColumnIndex;

            return columnIndex switch
            {
                3 => Colors.Blue,
                7 when gridCell.DataColumn.RowData is Model.OrderInfo row &&
                       string.Equals(row.PaymentStatus, "Not Paid", StringComparison.OrdinalIgnoreCase)
                    => Color.FromArgb("#C62828"),
                _ => Colors.Black
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
