using System.Text.Json.Serialization;
using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.UpdateRole;

public sealed class RoleUpdateRequest : IInjectable
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
    
    [JsonPropertyName("name")] public required string Name { get; init; }

    [property: JsonProperty("name")] public string Description { get; init; } = string.Empty;
}