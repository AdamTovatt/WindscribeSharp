using System.Reflection;

namespace Windscribe.Commands
{
    /// <summary>
    /// Represents a response from a command to Windscribe.
    /// </summary>
    public abstract class CommandResponse
    {
        /// <summary>
        /// The raw text returned by the command.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResponse"/> class.
        /// </summary>
        /// <param name="rawText">The raw text returned by the command.</param>
        /// <exception cref="InvalidDataException">Thrown if <paramref name="rawText"/> is null or empty.</exception>
        protected CommandResponse(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
                throw new InvalidDataException("Raw command response text is missing.");

            RawText = rawText;
        }

        /// <summary>
        /// Parses the raw command response into a dictionary of key-value pairs.
        /// </summary>
        /// <param name="rawText">The raw response text to parse.</param>
        /// <returns>A dictionary of keys and their corresponding values.</returns>
        protected static Dictionary<string, string> CreateResponseDictionary(string rawText)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            string[] lines = rawText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                int colonIndex = line.IndexOf(':');
                if (colonIndex < 0)
                    continue;

                string key = line.Substring(0, colonIndex).Trim();
                string value = line.Substring(colonIndex + 1).Trim();

                result[key] = value;
            }

            return result;
        }

        /// <summary>
        /// Populates all public instance properties decorated with <see cref="ResponseKeyAttribute"/> 
        /// using values from the provided dictionary. Only string properties are supported by default.
        /// </summary>
        /// <param name="values">A dictionary containing key-value pairs parsed from a command response.</param>
        /// <exception cref="InvalidDataException">
        /// Thrown if a required key defined by <see cref="ResponseKeyAttribute"/> is missing in the dictionary.
        /// </exception>
        protected void PopulatePropertiesFromDictionary(Dictionary<string, string> values)
        {
            Type type = GetType();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ResponseKeyAttribute? attribute = property.GetCustomAttribute<ResponseKeyAttribute>();
                if (attribute == null)
                    continue;

                if (!values.TryGetValue(attribute.Key, out string? raw) || raw == null)
                    throw new InvalidDataException($"Missing expected key: {attribute.Key}");

                object? value;

                if (attribute.ConverterType != null)
                {
                    if (Activator.CreateInstance(attribute.ConverterType) is not IResponseValueConverter converter)
                        throw new InvalidOperationException($"Converter for {property.Name} must implement IResponseValueConverter.");

                    value = converter.Convert(raw);
                }
                else if (property.PropertyType == typeof(string))
                {
                    value = raw;
                }
                else
                {
                    value = Convert.ChangeType(raw, property.PropertyType);
                }

                property.SetValue(this, value);
            }
        }
    }
}
