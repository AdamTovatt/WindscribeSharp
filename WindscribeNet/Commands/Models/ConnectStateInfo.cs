using WindscribeNet.Enums;

namespace WindscribeNet.Commands.Models
{
    public class ConnectStateInfo
    {
        public ConnectStateType State { get; }
        public string? City { get; }
        public bool HasNetworkInterference { get; }
        public string? ErrorMessage { get; }

        public ConnectStateInfo(
            ConnectStateType state,
            string? city = null,
            bool hasNetworkInterference = false,
            string? errorMessage = null)
        {
            State = state;
            City = city;
            HasNetworkInterference = hasNetworkInterference;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                return $"Error: {ErrorMessage}";

            if (State == ConnectStateType.Connected && !string.IsNullOrEmpty(City))
                return $"{(HasNetworkInterference ? "*" : "")}Connected: {City}" +
                       (HasNetworkInterference ? " [Network interference]" : "");

            if (State == ConnectStateType.Connecting && !string.IsNullOrEmpty(City))
                return $"Connecting: {City}";

            string prefix = HasNetworkInterference && State == ConnectStateType.Connected ? "*" : "";
            return $"{prefix}{EnumConverter.ToString(State)}";
        }
    }
}
