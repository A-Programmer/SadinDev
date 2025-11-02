using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Otp.SendOtp;

public class SendOtpRequest : IInjectable
{
    [JsonPropertyName("userNameOrEmailOrPhoneNumber")] public required string UserNameOrEmailOrPhoneNumber { get; set; }
}
