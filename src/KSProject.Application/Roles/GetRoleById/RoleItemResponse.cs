using Newtonsoft.Json;

namespace KSProject.Application.Roles.GetRoleById;

public sealed class RoleItemResponse
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
    
    [property:JsonProperty("name")] public required string Name { get; init; }
    
    [property:JsonProperty("description")] public required string Description { get; init; }
}