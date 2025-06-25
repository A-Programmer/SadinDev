using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.GetRoleById;

public sealed class RoleItemResponse
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
    
    [JsonPropertyName("name")] public required string Name { get; init; }
    
    [JsonPropertyName("description")] public required string Description { get; init; }
}