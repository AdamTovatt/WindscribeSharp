using WindscribeNet.Enums;

namespace WindscribeNet.Commands.ResponseParsing
{
    internal class ActiveStateConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            return EnumConverter.FromString<ActiveState>(value);
        }
    }
}
