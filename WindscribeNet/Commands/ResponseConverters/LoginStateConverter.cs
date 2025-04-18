using WindscribeNet.Commands.Models;
using WindscribeNet.Enums;

namespace WindscribeNet.Commands.ResponseConverters
{
    public class LoginStateConverter : IResponseValueConverter
    {
        public object Convert(string value)
        {
            if (value.Equals("Logged in", StringComparison.OrdinalIgnoreCase))
                return new LoginStateInfo(LoginStateType.LoggedIn);

            if (value.Equals("Logging in", StringComparison.OrdinalIgnoreCase))
                return new LoginStateInfo(LoginStateType.LoggingIn);

            if (value.Equals("Logged out", StringComparison.OrdinalIgnoreCase))
                return new LoginStateInfo(LoginStateType.LoggedOut);

            if (value.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
            {
                string errorMessage = value.Substring(6).Trim();

                LoginErrorCode errorCode = errorMessage.ToLowerInvariant() switch
                {
                    "no internet connectivity" => LoginErrorCode.NoInternet,
                    "no api connectivity" => LoginErrorCode.NoApi,
                    "incorrect username, password, or 2fa code" => LoginErrorCode.BadCredentials,
                    "need 2fa code" => LoginErrorCode.Missing2fa,
                    "ssl error" => LoginErrorCode.SslError,
                    "session expired" => LoginErrorCode.SessionExpired,
                    "rate limited" => LoginErrorCode.RateLimited,
                    "incorrect 2fa code" => LoginErrorCode.Bad2fa,
                    _ => string.IsNullOrWhiteSpace(errorMessage) ? LoginErrorCode.UnknownError : LoginErrorCode.CustomMessage
                };

                return new LoginStateInfo(LoginStateType.Error, errorCode, errorMessage);
            }

            throw new FormatException($"Unknown login state format: \"{value}\"");
        }
    }
}
