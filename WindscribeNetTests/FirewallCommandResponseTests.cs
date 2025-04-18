using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class FirewallCommandResponseTests
    {
        [TestMethod]
        public void ParseFirewallResponseIsOn()
        {
            FirewallCommandResponse response = new FirewallCommandResponse("Firewall is on.");
            Assert.AreEqual(ActiveState.On, response.State);
        }

        [TestMethod]
        public void ParseFirewallResponseIsAlreadyOn()
        {
            FirewallCommandResponse response = new FirewallCommandResponse("Firewall is already on.");
            Assert.AreEqual(ActiveState.On, response.State);
        }

        [TestMethod]
        public void ParseFirewallResponseIsOff()
        {
            FirewallCommandResponse response = new FirewallCommandResponse("Firewall is off.");
            Assert.AreEqual(ActiveState.Off, response.State);
        }

        [TestMethod]
        public void ParseFirewallResponseIsAlreadyOff()
        {
            FirewallCommandResponse response = new FirewallCommandResponse("Firewall is already off.");
            Assert.AreEqual(ActiveState.Off, response.State);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseFirewallResponseInvalidThrows()
        {
            _ = new FirewallCommandResponse("Completely unexpected output.");
        }
    }
}
