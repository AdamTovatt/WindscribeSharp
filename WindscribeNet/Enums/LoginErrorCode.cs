using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public enum LoginErrorCode
    {
        [EnumMember(Value = "No internet connectivity")]
        NoInternet,

        [EnumMember(Value = "No API connectivity")]
        NoApi,

        [EnumMember(Value = "Incorrect username, password, or 2FA code")]
        BadCredentials,

        [EnumMember(Value = "Need 2FA code")]
        Missing2fa,

        [EnumMember(Value = "SSL error")]
        SslError,

        [EnumMember(Value = "Session expired")]
        SessionExpired,

        [EnumMember(Value = "Rate limited")]
        RateLimited,

        [EnumMember(Value = "Incorrect 2FA code")]
        Bad2fa,

        UnknownError,

        CustomMessage
    }
}
