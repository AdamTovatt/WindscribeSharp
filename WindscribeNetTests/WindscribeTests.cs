using WindscribeNet;
using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class WindscribeTests
    {
        private ActiveState originalFirewallState = ActiveState.Off;

        //[TestInitialize]
        public async Task Init()
        {
            // Save firewall state
            StatusCommandResponse status = await Windscribe.GetStatusAsync();
            originalFirewallState = EnumConverter.FromString<ActiveState>(status.FirewallState.ToString());
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            StatusCommandResponse status = await Windscribe.GetStatusAsync();

            // Restore firewall state
            if (!originalFirewallState.ToString().Equals(status.FirewallState.ToString()))
            {
                await Windscribe.SetFirewallAsync(originalFirewallState);
                await Task.Delay(200);
            }
        }

        [TestMethod]
        public async Task GetStatusReturnsValidResult()
        {
            StatusCommandResponse status = await Windscribe.GetStatusAsync();

            Assert.IsNotNull(status);
            Assert.IsTrue(Enum.IsDefined(typeof(InternetConnectivity), status.InternetConnectivity));
            Assert.IsNotNull(status.LoginState);
            Assert.IsTrue(Enum.IsDefined(typeof(LoginStateType), status.LoginState.State));
            Assert.IsTrue(Enum.IsDefined(typeof(FirewallState), status.FirewallState));
            Assert.IsNotNull(status.ConnectState);
            Assert.IsTrue(Enum.IsDefined(typeof(ConnectStateType), status.ConnectState.State));
            Assert.IsTrue(status.DataUsage >= 0);
        }

        [TestMethod]
        public async Task SetFirewallToggles()
        {
            ActiveState flipped = originalFirewallState == ActiveState.On ? ActiveState.Off : ActiveState.On;

            FirewallCommandResponse setResponse = await Windscribe.SetFirewallAsync(flipped);
            Assert.AreEqual(flipped, setResponse.State);

            await Task.Delay(200);
            StatusCommandResponse confirmed = await Windscribe.GetStatusAsync();
            Assert.AreEqual(flipped.ToString(), confirmed.FirewallState.ToString());
        }
    }
}
