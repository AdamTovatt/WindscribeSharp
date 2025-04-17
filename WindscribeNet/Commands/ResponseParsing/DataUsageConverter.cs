using System.Globalization;

namespace Windscribe.Commands.ResponseParsing
{
    public class DataUsageConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            // Example: "8.04 GB / Unlimited" => return just the numeric GB part
            string[] parts = value.Split(' ');
            if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double gb))
                return gb;

            throw new FormatException("Invalid data usage format.");
        }
    }
}
