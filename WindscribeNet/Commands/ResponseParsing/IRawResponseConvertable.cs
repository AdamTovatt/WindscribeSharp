namespace Windscribe.Commands.ResponseParsing
{
    internal interface IRawResponseConvertable<T>
    {
        static abstract T FromRawText(string rawText);
    }
}
