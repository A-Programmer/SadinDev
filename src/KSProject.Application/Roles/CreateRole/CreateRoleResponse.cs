using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.CreateRole;

public sealed class CreateRoleResponse : IInjectable
{
    [property:JsonProperty("id")] public required Guid Id { get; init; }
}