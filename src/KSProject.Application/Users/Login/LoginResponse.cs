using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.Login;
// TODO: The login should return AccessToken and RefreshToken
public sealed class LoginResponse : IInjectable
{
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}