using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleResponse
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}