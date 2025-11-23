using System.Text.Json.Serialization;
using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Roles.UpdateRole;

public sealed class RoleUpdateRequest : IInjectable
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
    
    [JsonPropertyName("name")] public required string Name { get; init; }

    [property: JsonProperty("name")] public string Description { get; init; } = string.Empty;
}
