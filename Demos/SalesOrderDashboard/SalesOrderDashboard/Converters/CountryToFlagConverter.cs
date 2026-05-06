using System.Globalization;

namespace SalesOrderDashboard.Converters
{
    public class CountryToFlagConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string countryName)
            {
                string flagName = countryName.ToLower().Replace(" ", "");

                switch (flagName)
                {
                    case "usa": return "usa.png";
                    case "france": return "fr.png";
                    case "uk": return "uk.png";
                    case "germany": return "de.png";
                    case "canada": return "cd.png";
                    case "brazil": return "br.png";
                    case "japan": return "jp.png";
                    case "switzerland": return "ch.png";
                    case "spain": return "es.png";
                    case "mexico": return "mx.png";
                    case "italy": return "it.png";
                    default: return "default.png";
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
