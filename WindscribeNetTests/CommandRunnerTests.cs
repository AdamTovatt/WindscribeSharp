using WindscribeNet.CommandRunners;
using WindscribeNet.Commands;

namespace WindscribeTests
{
    [TestClass]
    public class CommandRunnerTests
    {
        [TestMethod]
        public async Task RunStatusCommand()
        {
            ICommandRunner commandRunner = new WindowsCommandRunner();
            Command statusCommand = new StatusCommand();

            StatusCommandResponse result = await commandRunner.RunAsync<StatusCommandResponse>(statusCommand);
        }
    }
}