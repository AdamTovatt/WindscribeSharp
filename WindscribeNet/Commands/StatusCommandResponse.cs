using Windscribe.Commands.ResponseParsing;
using Windscribe.Enums;

namespace Windscribe.Commands
{
    /// <summary>
    /// Represents the response from the Windscribe "status" command.
    /// </summary>
    internal class StatusCommandResponse : CommandResponse, IRawResponseConvertable<StatusCommandResponse>
    {
        [ResponseKey("Internet connectivity")]
        public string InternetConnectivity { get; private set; } = string.Empty;

        [ResponseKey("Login state")]
        public string LoginState { get; private set; } = string.Empty;

        [ResponseKey("Firewall state", typeof(ActiveStateConverter))]
        public ActiveState FirewallState { get; private set; }

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
