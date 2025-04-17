[AttributeUsage(AttributeTargets.Property)]
public sealed class ResponseKeyAttribute : Attribute
{
    public string Key { get; }
    public Type? ConverterType { get; }

    public ResponseKeyAttribute(string key)
    {
        Key = key;
    }

    public ResponseKeyAttribute(string key, Type converterType)
    {
        Key = key;
        ConverterType = converterType;
    }
}
