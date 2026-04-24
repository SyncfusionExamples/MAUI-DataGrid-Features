using Syncfusion.Maui.DataGrid;
using System.Globalization;

namespace Styling
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

    }

    public class ColorConverter : IValueConverter
    {
        object IValueConverter.Convert(object? value, Type? targetType, object? parameter, CultureInfo info)
        {
            var dataGridRow = value as DataGridRow;
            var rowIndex = dataGridRow.DataRow.RowIndex;
            int colorIndex = (rowIndex - 1) % 5;

            switch (colorIndex)
            {
                case 0:
                    return Color.FromArgb("#c0efe0");

                case 1:
                    return Color.FromArgb("#fff6c8");

                case 2:
                    return Color.FromArgb("#feeba8");

                case 3:
                    return Color.FromArgb("#fef6c8");

                case 4:
                    return Color.FromArgb("#ffe5c0");
            }
            return Colors.White;

        }
        public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StockStatusToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            return value.ToString() switch
            {
                "In Stock" => Color.FromArgb("#16A34A"),   // Green
                "Low Stock" => Color.FromArgb("#feae2c"), // Orange
                "Out of Stock" => Color.FromArgb("#DC2626"), // Red (optional)
                _ => Color.FromArgb("#64748B") // Default gray
            };
        }

        public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
