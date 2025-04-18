using WindscribeNet.Commands.Models;
using WindscribeNet.Commands.ResponseConverters;
using WindscribeNet.Commands.ResponseParsing;
using WindscribeNet.Enums;

namespace WindscribeNet.Commands
{
    /// <summary>
    /// Represents the response from the Windscribe "status" command.
    /// </summary>
    public class StatusCommandResponse : CommandResponse, IRawResponseConvertable<StatusCommandResponse>
    {
        [ResponseKey("Internet connectivity", typeof(InternetConnectivityConverter))]
        public InternetConnectivity InternetConnectivity { get; private set; }

        [ResponseKey("Login state", typeof(LoginStateConverter))]
        public LoginStateInfo LoginState { get; private set; }

        [ResponseKey("Firewall state", typeof(FirewallStateConverter))]
        public FirewallState FirewallState { get; private set; }

        [ResponseKey("Connect state", typeof(ConnectStateConverter))]
        public ConnectStateInfo ConnectState { get; private set; }

        [ResponseKey("Data usage", typeof(DataUsageConverter))]
        public double DataUsage { get; private set; }

        public StatusCommandResponse(string rawText) : base(rawText)
        {
            Dictionary<string, string> values = CreateResponseDictionary(rawText);
            PopulatePropertiesFromDictionary(values);

            if (LoginState == null)
                throw new InvalidDataException($"Conversion of {rawText} lead to missing LoginState");

            if (ConnectState == null)
                throw new InvalidDataException($"Conversion of {rawText} lead to missing LoginState");
        }

        public static StatusCommandResponse FromRawText(string raw)
        {
            return new StatusCommandResponse(raw);
        }
    }
}
