namespace WindscribeNet.Commands
{
    internal class ConnectCommand : Command
    {
        internal override string Name => "connect";

        private readonly string? location;
        private readonly bool isStatic;
        private readonly string? protocol;
        private readonly bool nonBlocking;

        internal ConnectCommand(string? location = null, bool isStatic = false, string? protocol = null, bool nonBlocking = false)
        {
            this.location = location;
            this.isStatic = isStatic;
            this.protocol = protocol;
            this.nonBlocking = nonBlocking;
        }

        public override string ToString()
        {
            List<string> args = new() { Name };

            if (isStatic)
                args.Add("static");

            if (!string.IsNullOrWhiteSpace(location))
                args.Add($"\"{location}\"");

            if (!string.IsNullOrWhiteSpace(protocol))
                args.Add(protocol);

            if (nonBlocking)
                args.Add("-n");

            return string.Join(' ', args);
        }
    }
}
