using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Roles.DeleteRole;

public sealed class DeleteRoleRequest : IInjectable
{
    [JsonPropertyName("id")] public required Guid id { get; init; }
}
