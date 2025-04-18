using WindscribeNet.Commands.Models;
using WindscribeNet.Commands.ResponseConverters;
using WindscribeNet.Enums;

namespace WindscribeNetTests
{
    [TestClass]
    public class LoginStateConversionTests
    {
        private readonly LoginStateConverter converter = new LoginStateConverter();

        [TestMethod]
        public void ConvertLoginStateLoggedIn()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("Logged in");

            Assert.AreEqual(LoginStateType.LoggedIn, result.State);
            Assert.IsNull(result.ErrorCode);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Logged in", result.ToString());
        }

        [TestMethod]
        public void ConvertLoginStateLoggedOut()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("Logged out");

            Assert.AreEqual(LoginStateType.LoggedOut, result.State);
            Assert.IsNull(result.ErrorCode);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Logged out", result.ToString());
        }

        [TestMethod]
        public void ConvertLoginStateLoggingIn()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("Logging in");

            Assert.AreEqual(LoginStateType.LoggingIn, result.State);
            Assert.IsNull(result.ErrorCode);
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual("Logging in", result.ToString());
        }

        [TestMethod]
        public void ConvertLoginStateKnownError()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("Error: SSL error");

            Assert.AreEqual(LoginStateType.Error, result.State);
            Assert.AreEqual(LoginErrorCode.SslError, result.ErrorCode);
            Assert.AreEqual("SSL error", result.ErrorMessage);
            Assert.AreEqual("Error: SSL error", result.ToString());
        }

        [TestMethod]
        public void ConvertLoginStateCustomError()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("Error: Something unexpected");

            Assert.AreEqual(LoginStateType.Error, result.State);
            Assert.AreEqual(LoginErrorCode.CustomMessage, result.ErrorCode);
            Assert.AreEqual("Something unexpected", result.ErrorMessage);
            Assert.AreEqual("Error: Something unexpected", result.ToString());
        }

        [TestMethod]
        public void ConvertLoginStateErrorCaseInsensitive()
        {
            LoginStateInfo result = (LoginStateInfo)converter.Convert("error: ssl error");

            Assert.AreEqual(LoginStateType.Error, result.State);
            Assert.AreEqual(LoginErrorCode.SslError, result.ErrorCode);
            Assert.AreEqual("ssl error", result.ErrorMessage);
            Assert.AreEqual("Error: ssl error", result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertLoginStateInvalidInputThrows()
        {
            converter.Convert("Invalid input");
        }
    }
}
