using Newtonsoft.Json;

namespace KSProject.Application.Roles.UpdateRole;

public sealed class RoleUpdateResponse
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
}