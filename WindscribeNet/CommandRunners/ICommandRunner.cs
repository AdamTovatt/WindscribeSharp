using WindscribeNet.Commands;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.CommandRunners
{
    internal interface ICommandRunner
    {
        Task<T> RunAsync<T>(Command command)
            where T : CommandResponse, IRawResponseConvertable<T>;
    }
}
