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

        [TestMethod]
        public async Task RunFirewallCommandToggleAndRevert()
        {
            ICliRunner cliRunner = CliRunnerProvider.Instance;

            // Get initial state
            StatusCommandResponse initialStatus = await cliRunner.RunAsync<StatusCommandResponse>(new StatusCommand());
            ActiveState originalState = EnumConverter.FromString<ActiveState>(initialStatus.FirewallState.ToString());

            await Task.Delay(200);

            // Flip the state
            ActiveState toggledState = originalState == ActiveState.On ? ActiveState.Off : ActiveState.On;
            FirewallCommand toggleCommand = new FirewallCommand(toggledState);
            FirewallCommandResponse toggleResponse = await cliRunner.RunAsync<FirewallCommandResponse>(toggleCommand);

            await Task.Delay(200);

            // Verify new state via response
            Assert.AreEqual(toggledState, toggleResponse.State);

            await Task.Delay(200);

            // Confirm via status
            StatusCommandResponse afterToggleStatus = await cliRunner.RunAsync<StatusCommandResponse>(new StatusCommand());
            Assert.AreEqual(toggledState.ToString(), afterToggleStatus.FirewallState.ToString());

            await Task.Delay(200);

            // Revert to original
            FirewallCommand revertCommand = new FirewallCommand(originalState);
            FirewallCommandResponse revertResponse = await cliRunner.RunAsync<FirewallCommandResponse>(revertCommand);

            await Task.Delay(200);

            // Confirm reverted
            StatusCommandResponse finalStatus = await cliRunner.RunAsync<StatusCommandResponse>(new StatusCommand());
            Assert.AreEqual(originalState.ToString(), finalStatus.FirewallState.ToString());
        }
    }
}