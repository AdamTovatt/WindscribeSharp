using System.Reflection;
using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public static class EnumConverter
    {
        private static readonly Dictionary<Type, Dictionary<string, object>> _fromStringCache = new();
        private static readonly Dictionary<Type, Dictionary<object, string>> _toStringCache = new();

        public static T FromString<T>(string value) where T : struct, Enum
        {
            Type enumType = typeof(T);

            if (!_fromStringCache.TryGetValue(enumType, out Dictionary<string, object>? map))
            {
                map = enumType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .ToDictionary(
                        f => f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f.Name,
                        f => (object)f.GetValue(null)!,
                        StringComparer.OrdinalIgnoreCase
                    );

                _fromStringCache[enumType] = map;
            }

            if (map.TryGetValue(value, out object? result))
                return (T)result;

            throw new ArgumentException($"Unknown value '{value}' for enum '{enumType.Name}'");
        }

        public static string ToString<T>(T value) where T : struct, Enum
        {
            Type enumType = typeof(T);

            if (!_toStringCache.TryGetValue(enumType, out Dictionary<object, string>? map))
            {
                map = enumType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .ToDictionary(
                        f => (object)f.GetValue(null)!,
                        f => f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f.Name
                    );

                _toStringCache[enumType] = map;
            }

            if (map.TryGetValue(value, out string? result))
                return result;

            throw new ArgumentException($"Unknown enum value '{value}' for enum '{enumType.Name}'");
        }
    }
}
