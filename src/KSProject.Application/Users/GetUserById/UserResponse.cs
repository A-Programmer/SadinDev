using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.GetUserById;

public sealed class UserResponse: IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("userName")] public string UserName { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("phoneNumber")] public string PhoneNumber { get; set; }
    [JsonPropertyName("roles")] public List<string> Roles { get; set; }
}