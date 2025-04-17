namespace WindscribeNet.Commands
{
    internal abstract class Command
    {
        internal abstract string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
