using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public enum LoginStateType
    {
        [EnumMember(Value = "Logged out")]
        LoggedOut,

        [EnumMember(Value = "Logging in")]
        LoggingIn,

        [EnumMember(Value = "Logged in")]
        LoggedIn,

        [EnumMember(Value = "Error")]
        Error
    }
}
