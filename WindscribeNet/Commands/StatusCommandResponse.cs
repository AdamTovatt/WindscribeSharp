using WindscribeNet.Commands.ResponseParsing;
using WindscribeNet.Enums;

namespace WindscribeNet.Commands
{
    /// <summary>
    /// Represents the response from the Windscribe "status" command.
    /// </summary>
    internal class StatusCommandResponse : CommandResponse, IRawResponseConvertable<StatusCommandResponse>
    {
        [ResponseKey("Internet connectivity", typeof(InternetConnectivityConverter))]
        public InternetConnectivity InternetConnectivity { get; private set; }

        [ResponseKey("Login state")]
        public string LoginState { get; private set; } = string.Empty;

        [ResponseKey("Firewall state", typeof(FirewallStateConverter))]
        public FirewallState FirewallState { get; private set; }

        [ResponseKey("Connect state")]
        public string ConnectState { get; private set; } = string.Empty;

        [ResponseKey("Data usage", typeof(DataUsageConverter))]
        public double DataUsage { get; private set; }

        public StatusCommandResponse(string rawText) : base(rawText)
        {
            Dictionary<string, string> values = CreateResponseDictionary(rawText);
            PopulatePropertiesFromDictionary(values);
        }

        public static StatusCommandResponse FromRawText(string raw)
        {
            return new StatusCommandResponse(raw);
        }
    }
}
