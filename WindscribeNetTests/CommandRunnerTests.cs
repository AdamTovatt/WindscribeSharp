using WindscribeNet.CliRunners;
using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class CommandRunnerTests
    {
        [TestMethod]
        public async Task RunStatusCommand()
        {
            ICliRunner cliRunner = CliRunnerProvider.Instance;
            Command statusCommand = new StatusCommand();

            StatusCommandResponse result = await cliRunner.RunAsync<StatusCommandResponse>(statusCommand);

            Assert.IsNotNull(result);
            Assert.IsTrue(Enum.IsDefined(typeof(InternetConnectivity), result.InternetConnectivity));

            Assert.IsNotNull(result.LoginState);
            Assert.IsTrue(Enum.IsDefined(typeof(LoginStateType), result.LoginState.State));

            Assert.IsTrue(Enum.IsDefined(typeof(FirewallState), result.FirewallState));

            Assert.IsNotNull(result.ConnectState);
            Assert.IsTrue(Enum.IsDefined(typeof(ConnectStateType), result.ConnectState.State));

            Assert.IsTrue(result.DataUsage >= 0);
        }
    }
}