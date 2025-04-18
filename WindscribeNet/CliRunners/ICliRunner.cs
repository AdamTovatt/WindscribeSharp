using WindscribeNet.Commands;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.CliRunners
{
    internal interface ICliRunner
    {
        Task<T> RunAsync<T>(Command command)
            where T : CommandResponse, IRawResponseConvertable<T>;
    }
}
