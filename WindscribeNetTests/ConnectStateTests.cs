using WindscribeNet.Commands.Models;
using WindscribeNet.Commands.ResponseConverters;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class ConnectStateTests
    {
        private readonly ConnectStateConverter converter = new ConnectStateConverter();

        [TestMethod]
        public void ConvertConnectStateConnectedWithoutCity()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Connected");

            Assert.AreEqual(ConnectStateType.Connected, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Connected", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateConnectedWithCity()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Connected: Stockholm");

            Assert.AreEqual(ConnectStateType.Connected, result.State);
            Assert.AreEqual("Stockholm", result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Connected: Stockholm", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateConnectedWithCityAndInterference()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("*Connected: Stockholm [Network interference]");

            Assert.AreEqual(ConnectStateType.Connected, result.State);
            Assert.AreEqual("Stockholm", result.City);
            Assert.IsTrue(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("*Connected: Stockholm [Network interference]", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateConnecting()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Connecting");

            Assert.AreEqual(ConnectStateType.Connecting, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Connecting", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateConnectingWithCity()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Connecting: Berlin");

            Assert.AreEqual(ConnectStateType.Connecting, result.State);
            Assert.AreEqual("Berlin", result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Connecting: Berlin", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateDisconnecting()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Disconnecting");

            Assert.AreEqual(ConnectStateType.Disconnecting, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Disconnecting", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateDisconnected()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Disconnected");

            Assert.AreEqual(ConnectStateType.Disconnected, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Disconnected", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateDisconnectedWithError()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Error: WireGuard adapter setup failed");

            Assert.AreEqual(ConnectStateType.Disconnected, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.AreEqual("WireGuard adapter setup failed", result.ErrorMessage);
            Assert.AreEqual("Error: WireGuard adapter setup failed", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateDisconnectedWithLongReason()
        {
            string input = "Disconnected due to reaching WireGuard key limit. Use \"windscribe-cli keylimit delete\"...";
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert(input);

            Assert.AreEqual(ConnectStateType.Disconnected, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.AreEqual(input, result.ErrorMessage);
            Assert.AreEqual($"Error: {input}", result.ToString());
        }

        [TestMethod]
        public void ConvertConnectStateUnknown()
        {
            ConnectStateInfo result = (ConnectStateInfo)converter.Convert("Unknown state");

            Assert.AreEqual(ConnectStateType.Unknown, result.State);
            Assert.IsNull(result.City);
            Assert.IsFalse(result.HasNetworkInterference);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Unknown state", result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertConnectStateInvalidInputThrows()
        {
            converter.Convert("Some nonsense");
        }
    }
}
