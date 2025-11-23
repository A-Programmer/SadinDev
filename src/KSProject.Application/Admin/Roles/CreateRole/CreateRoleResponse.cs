using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Roles.CreateRole;

public sealed class CreateRoleResponse : IInjectable
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}
