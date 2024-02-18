using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ABCBank.Domain.Categories
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountStatus
    {
        [EnumMember(Value="Active")]
        Active,
        [EnumMember(Value="InActive")]
        InActive,
        [EnumMember(Value="Deleted")]
        Deleted,
        [EnumMember(Value="Suspended")]
        Suspended
    }
}