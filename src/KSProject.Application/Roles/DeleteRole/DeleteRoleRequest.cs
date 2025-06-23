using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleRequest : IInjectable
{
    [property:JsonProperty("id")] public required Guid id { get; init; }
}