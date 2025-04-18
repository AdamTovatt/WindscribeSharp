using WindscribeNet.Commands.Models;
using WindscribeNet.Commands.ResponseConverters;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.Commands
{
    public class ConnectCommandResponse : CommandResponse, IRawResponseConvertable<ConnectCommandResponse>
    {
        public ConnectStateInfo ConnectState { get; }

        public ConnectCommandResponse(string rawText) : base(rawText)
        {
            string lastLine = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Last().Trim();
            ConnectState = (ConnectStateInfo)new ConnectStateConverter().Convert(lastLine);
        }

        public static ConnectCommandResponse FromRawText(string raw)
        {
            return new ConnectCommandResponse(raw);
        }
    }
}
