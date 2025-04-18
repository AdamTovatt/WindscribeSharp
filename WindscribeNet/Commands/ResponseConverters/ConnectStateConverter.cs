using WindscribeNet.Commands.Models;
using WindscribeNet.Enums;

namespace WindscribeNet.Commands.ResponseConverters
{
    internal class ConnectStateConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            string raw = value.Trim();
            bool hasInterference = raw.Contains("[Network interference]");
            string cleaned = raw.Replace("[Network interference]", "").TrimStart('*').Trim();

            if (cleaned.StartsWith("Connected:", StringComparison.OrdinalIgnoreCase))
            {
                string city = cleaned.Substring("Connected:".Length).Trim();
                return new ConnectStateInfo(ConnectStateType.Connected, city, hasInterference);
            }

            if (cleaned.Equals("Connected", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Connected, null, hasInterference);
            }

            if (cleaned.StartsWith("Connecting:", StringComparison.OrdinalIgnoreCase))
            {
                string city = cleaned.Substring("Connecting:".Length).Trim();
                return new ConnectStateInfo(ConnectStateType.Connecting, city);
            }

            if (cleaned.Equals("Connecting", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Connecting);
            }

            if (cleaned.Equals("Disconnecting", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Disconnecting);
            }

            if (cleaned.StartsWith("Disconnected due to", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Disconnected, errorMessage: cleaned);
            }

            if (cleaned.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
            {
                string errorMessage = cleaned.Substring("Error:".Length).Trim();
                return new ConnectStateInfo(ConnectStateType.Disconnected, errorMessage: errorMessage);
            }

            if (cleaned.Equals("Disconnected", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Disconnected);
            }

            if (cleaned.Equals("Unknown state", StringComparison.OrdinalIgnoreCase))
            {
                return new ConnectStateInfo(ConnectStateType.Unknown);
            }

            throw new FormatException($"Unknown connect state format: \"{value}\"");
        }
    }
}
