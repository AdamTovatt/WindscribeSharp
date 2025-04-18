using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class CommandResponseParsingTests
    {
        [TestMethod]
        public void CreateStatusCommandResponse()
        {
            const string statusResponseString =
                "Internet connectivity: available\r\n" +
                "Login state: Logged in\r\n" +
                "Firewall state: Off\r\n" +
                "Connect state: Disconnected\r\n" +
                "Data usage: 8.04 GB / Unlimited\r\n";

            StatusCommandResponse status = new StatusCommandResponse(statusResponseString);

            Assert.IsNotNull(status);
            Assert.AreEqual(InternetConnectivity.Available, status.InternetConnectivity);
            Assert.IsNotNull(status.LoginState);
            Assert.AreEqual(LoginStateType.LoggedIn, status.LoginState.State);
            Assert.IsNull(status.LoginState.ErrorCode);
            Assert.IsNull(status.LoginState.ErrorMessage);
            Assert.AreEqual(FirewallState.Off, status.FirewallState);
            Assert.IsNotNull(status.ConnectState);
            Assert.AreEqual(ConnectStateType.Disconnected, status.ConnectState.State);
            Assert.IsNull(status.ConnectState.City);
            Assert.IsFalse(status.ConnectState.HasNetworkInterference);
            Assert.IsNull(status.ConnectState.ErrorMessage);
            Assert.AreEqual(8.04, status.DataUsage, 0.001);
        }

        [TestMethod]
        public void CreateStatusCommandResponseWithEdgeCases()
        {
            const string statusResponseString =
                "Internet connectivity: unavailable\r\n" +
                "Login state: Error: SSL error\r\n" +
                "Firewall state: Always On\r\n" +
                "Connect state: *Connected: London [Network interference]\r\n" +
                "Data usage: 0.00 GB / 10 GB\r\n";

            StatusCommandResponse status = new StatusCommandResponse(statusResponseString);

            Assert.AreEqual(InternetConnectivity.Unavailable, status.InternetConnectivity);

            Assert.IsNotNull(status.LoginState);
            Assert.AreEqual(LoginStateType.Error, status.LoginState.State);
            Assert.AreEqual(LoginErrorCode.SslError, status.LoginState.ErrorCode);
            Assert.AreEqual("SSL error", status.LoginState.ErrorMessage);

            Assert.AreEqual(FirewallState.AlwaysOn, status.FirewallState);

            Assert.IsNotNull(status.ConnectState);
            Assert.AreEqual(ConnectStateType.Connected, status.ConnectState.State);
            Assert.AreEqual("London", status.ConnectState.City);
            Assert.IsTrue(status.ConnectState.HasNetworkInterference);
            Assert.IsNull(status.ConnectState.ErrorMessage);

            Assert.AreEqual(0.00, status.DataUsage, 0.001);
        }
    }
}
