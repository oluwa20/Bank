using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ABCBank.Domain.Categories
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        [EnumMember(Value = "Savings")]
        Savings,

        [EnumMember(Value = "Current")]
        Current,

        [EnumMember(Value = "Joint")]
        Joint,

        [EnumMember(Value = "Corporate")]
        Corporate,

        [EnumMember(Value = "FixedDeposit")]
        FixedDeposit,

        [EnumMember(Value = "Salary")]
        Salary,

        [EnumMember(Value = "Student")]
        Student,

        [EnumMember(Value = "Domiciliary")]
        Domiciliary,

        [EnumMember(Value = "Premium")]
        Premium,

        [EnumMember(Value = "Gold")]
        Gold,

        [EnumMember(Value = "Platinum")]
        Platinum,
    }
}
