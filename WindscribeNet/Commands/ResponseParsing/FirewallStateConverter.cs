using WindscribeNet.Enums;

namespace WindscribeNet.Commands.ResponseParsing
{
    public class FirewallStateConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            return EnumConverter.FromString<FirewallState>(value);
        }
    }
}
