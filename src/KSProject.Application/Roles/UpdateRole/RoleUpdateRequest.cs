using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.UpdateRole;

public sealed class RoleUpdateRequest : IInjectable
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
    
    [property:JsonProperty("name")] public required string Name { get; init; }

    [property: JsonProperty("name")] public string Description { get; init; } = string.Empty;
}