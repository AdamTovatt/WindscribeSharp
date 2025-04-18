using WindscribeNet.Enums;

namespace WindscribeNet.Commands
{
    internal class FirewallCommand : Command
    {
        internal override string Name => "firewall";

        private readonly ActiveState state;

        internal FirewallCommand(ActiveState state)
        {
            this.state = state;
        }

        public override string ToString()
        {
            return $"{Name} {state.ToString().ToLower()}";
        }
    }
}
