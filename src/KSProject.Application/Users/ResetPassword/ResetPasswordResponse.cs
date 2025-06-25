using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.ResetPassword;

public sealed class ResetPasswordResponse : IInjectable
{
    [JsonPropertyName("result")] public bool Result { get; set; }
}