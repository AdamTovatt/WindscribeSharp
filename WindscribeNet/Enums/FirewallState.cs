using System.Runtime.Serialization;

namespace WindscribeNet.Enums
{
    public enum FirewallState
    {
        [EnumMember(Value = "Off")]
        Off,

        [EnumMember(Value = "On")]
        On,

        [EnumMember(Value = "Always On")]
        AlwaysOn
    }
}
