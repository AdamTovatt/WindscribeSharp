using WindscribeNet.Enums;

namespace WindscribeNet.Commands.ResponseParsing
{
    public class InternetConnectivityConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            return EnumConverter.FromString<InternetConnectivity>(value);
        }
    }
}
