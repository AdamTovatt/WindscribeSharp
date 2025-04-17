using Windscribe.Enums;

namespace Windscribe.Commands.ResponseParsing
{
    internal class ActiveStateConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            return ActiveStateEnumConverter.FromString(value);
        }
    }
}
