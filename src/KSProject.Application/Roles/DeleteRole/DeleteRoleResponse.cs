using Newtonsoft.Json;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleResponse
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
}