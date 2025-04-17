using WindscribeNet.Enums;

namespace WindscribeTests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void ConvertActiveState()
        {
            Assert.AreEqual("On", EnumConverter.ToString(ActiveState.On));
            Assert.AreEqual("Off", EnumConverter.ToString(ActiveState.Off));

            Assert.AreEqual(ActiveState.On, EnumConverter.FromString<ActiveState>("On"));
            Assert.AreEqual(ActiveState.On, EnumConverter.FromString<ActiveState>("on"));
            Assert.AreEqual(ActiveState.On, EnumConverter.FromString<ActiveState>("ON"));

            Assert.AreEqual(ActiveState.Off, EnumConverter.FromString<ActiveState>("Off"));
            Assert.AreEqual(ActiveState.Off, EnumConverter.FromString<ActiveState>("off"));
            Assert.AreEqual(ActiveState.Off, EnumConverter.FromString<ActiveState>("OFF"));
        }

        [TestMethod]
        public void ConvertInternetConnectivity()
        {
            Assert.AreEqual("available", EnumConverter.ToString(InternetConnectivity.Available));
            Assert.AreEqual("unavailable", EnumConverter.ToString(InternetConnectivity.Unavailable));

            Assert.AreEqual(InternetConnectivity.Available, EnumConverter.FromString<InternetConnectivity>("available"));
            Assert.AreEqual(InternetConnectivity.Available, EnumConverter.FromString<InternetConnectivity>("AVAILABLE"));

            Assert.AreEqual(InternetConnectivity.Unavailable, EnumConverter.FromString<InternetConnectivity>("unavailable"));
            Assert.AreEqual(InternetConnectivity.Unavailable, EnumConverter.FromString<InternetConnectivity>("UNAVAILABLE"));
        }

        [TestMethod]
        public void ConvertFirewallState()
        {
            Assert.AreEqual("Off", EnumConverter.ToString(FirewallState.Off));
            Assert.AreEqual("On", EnumConverter.ToString(FirewallState.On));
            Assert.AreEqual("Always On", EnumConverter.ToString(FirewallState.AlwaysOn));

            Assert.AreEqual(FirewallState.Off, EnumConverter.FromString<FirewallState>("off"));
            Assert.AreEqual(FirewallState.On, EnumConverter.FromString<FirewallState>("on"));
            Assert.AreEqual(FirewallState.AlwaysOn, EnumConverter.FromString<FirewallState>("always on"));
        }
    }
}
