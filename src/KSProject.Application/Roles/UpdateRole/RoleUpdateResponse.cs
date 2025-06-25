using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.UpdateRole;

public sealed class RoleUpdateResponse
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}