namespace Windscribe.Enums
{
    public static class ActiveStateEnumConverter
    {
        public static ActiveState FromString(string value)
        {
            if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase))
                return ActiveState.On;
            else if (string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
                return ActiveState.Off;

            throw new ArgumentException($"Can not convert string value \"{value}\" to ActiveState enum");
        }

        public static string ToString(ActiveState activeState)
        {
            return activeState.ToString();
        }
    }
}
