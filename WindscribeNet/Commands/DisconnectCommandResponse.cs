using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.Commands
{
    public class DisconnectCommandResponse : CommandResponse, IRawResponseConvertable<DisconnectCommandResponse>
    {
        public bool WasDisconnected { get; }

        public DisconnectCommandResponse(string rawText) : base(rawText)
        {
            WasDisconnected = rawText.Contains("Disconnected", StringComparison.OrdinalIgnoreCase);
        }

        public static DisconnectCommandResponse FromRawText(string raw)
        {
            return new DisconnectCommandResponse(raw);
        }
    }
}
