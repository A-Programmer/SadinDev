using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.DeleteRole;

public sealed class DeleteRoleRequest : IInjectable
{
    [JsonPropertyName("id")] public required Guid id { get; init; }
}