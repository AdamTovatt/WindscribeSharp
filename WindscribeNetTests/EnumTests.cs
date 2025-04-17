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
    }
}
