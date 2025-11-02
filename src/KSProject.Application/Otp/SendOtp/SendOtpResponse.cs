using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Otp.SendOtp;

public sealed class SendOtpResponse : IInjectable
{
    [JsonPropertyName("userNameOrEmailOrPhoneNumber")]
    public string UserNameOrEmailOrPhoneNumber { get; set; } = string.Empty;
}
