using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public enum ConnectStateType
    {
        [EnumMember(Value = "Connected")]
        Connected,

        [EnumMember(Value = "Connecting")]
        Connecting,

        [EnumMember(Value = "Disconnecting")]
        Disconnecting,

        [EnumMember(Value = "Disconnected")]
        Disconnected,

        [EnumMember(Value = "Unknown state")]
        Unknown
    }
}
