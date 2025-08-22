using KSFramework.Contracts;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.Login;
// TODO: The login should return AccessToken and RefreshToken
public sealed class LoginResponse : IInjectable
{
	[JsonPropertyName("access_token")]
	public required string Access_Token { get; set; }
	[JsonPropertyName("refresh_token")]
	public required string Refresh_Token { get; set; }
	[JsonPropertyName("expire_at")]
	public required DateTimeOffset Expire_At { get; set; }
}