using WindscribeNet.Commands.ResponseParsing;
using WindscribeNet.Enums;

namespace WindscribeNet.Commands
{
    public class FirewallCommandResponse : CommandResponse, IRawResponseConvertable<FirewallCommandResponse>
    {
        public ActiveState State { get; }

        public FirewallCommandResponse(string rawText) : base(rawText)
        {
            string[] words = rawText.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string lastWord = words.Last().TrimEnd('.');

            State = (ActiveState)new ActiveStateConverter().Convert(lastWord);
        }

        public static FirewallCommandResponse FromRawText(string raw)
        {
            return new FirewallCommandResponse(raw);
        }
    }
}
