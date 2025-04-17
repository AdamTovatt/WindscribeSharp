using Windscribe.Commands;
using Windscribe.Commands.ResponseParsing;

namespace Windscribe.CommandRunners
{
    internal interface ICommandRunner
    {
        Task<T> RunAsync<T>(Command command)
            where T : CommandResponse, IRawResponseConvertable<T>;
    }
}
