namespace WindscribeNet.Commands.ResponseParsing
{
    internal interface IRawResponseConvertable<T>
    {
        static abstract T FromRawText(string rawText);
    }
}
