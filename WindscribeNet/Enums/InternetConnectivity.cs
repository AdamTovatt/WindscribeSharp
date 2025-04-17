using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public enum InternetConnectivity
    {
        [EnumMember(Value = "available")]
        Available,

        [EnumMember(Value = "unavailable")]
        Unavailable
    }
}
