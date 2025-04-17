using WindscribeNet.Commands;

namespace WindscribeTests
{
    [TestClass]
    public class CommandResponseParsingTests
    {
        [TestMethod]
        public void CreateStatusCommandResponse()
        {
            const string statusResponseString = "Internet connectivity: available\r\nLogin state: Logged in\r\nFirewall state: Off\r\nConnect state: Disconnected\r\nData usage: 8.04 GB / Unlimited\r\n";

            StatusCommandResponse status = new StatusCommandResponse(statusResponseString);

            Assert.IsNotNull(status);
        }
    }
}
