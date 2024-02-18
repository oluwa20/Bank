using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ABCBank.Domain.Categories
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TokenType
    {
        [EnumMember(Value = "PasswordReset")]
        PasswordReset,

        [EnumMember(Value = "EmailVerification")]
        EmailVerification,

        [EnumMember(Value = "TwoFactorAuthentication")]
        TwoFactorAuthentication,

        [EnumMember(Value = "AccountActivation")]
        AccountActivation,

        [EnumMember(Value = "PasswordChange")]
        PasswordChange,

  
    }
}
