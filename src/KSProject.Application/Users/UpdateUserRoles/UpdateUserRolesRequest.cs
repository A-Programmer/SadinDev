using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.UpdateUserRoles;

public sealed class UpdateUserRolesRequest : IInjectable
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("roles")]
    public string[] Roles { get; set; }
}