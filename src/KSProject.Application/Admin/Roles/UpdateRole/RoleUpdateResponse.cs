using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.Roles.UpdateRole;

public sealed class RoleUpdateResponse
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}
