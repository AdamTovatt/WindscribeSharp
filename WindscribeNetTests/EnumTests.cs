using Windscribe.Enums;

namespace WindscribeTests
{
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void ConvertActiveState()
        {
            Assert.AreEqual("On", ActiveStateEnumConverter.ToString(ActiveState.On));
            Assert.AreEqual("Off", ActiveStateEnumConverter.ToString(ActiveState.Off));

            Assert.AreEqual(ActiveState.On, ActiveStateEnumConverter.FromString("On"));
            Assert.AreEqual(ActiveState.On, ActiveStateEnumConverter.FromString("on"));
            Assert.AreEqual(ActiveState.On, ActiveStateEnumConverter.FromString("ON"));

            Assert.AreEqual(ActiveState.Off, ActiveStateEnumConverter.FromString("Off"));
            Assert.AreEqual(ActiveState.Off, ActiveStateEnumConverter.FromString("off"));
            Assert.AreEqual(ActiveState.Off, ActiveStateEnumConverter.FromString("OFF"));
        }
    }
}
