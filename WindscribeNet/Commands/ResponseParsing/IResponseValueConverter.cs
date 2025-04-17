namespace Windscribe.Commands
{
    public interface IResponseValueConverter
    {
        object Convert(string value);
    }
}
