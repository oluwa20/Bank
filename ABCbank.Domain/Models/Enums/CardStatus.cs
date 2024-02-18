using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ABCBank.Domain.Categories
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CardStatus
    {
        [EnumMember(Value = "Active")]
        Active,

        [EnumMember(Value = "Inactive")]
        Inactive,

        [EnumMember(Value = "Blocked")]
        Blocked,

        [EnumMember(Value = "Lost")]
        Lost,

        [EnumMember(Value = "Stolen")]
        Stolen,

        [EnumMember(Value = "Expired")]
        Expired
    }
}
