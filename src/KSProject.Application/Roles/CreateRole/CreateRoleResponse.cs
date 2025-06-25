using KSFramework.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.CreateRole;

public sealed class CreateRoleResponse : IInjectable
{
    [JsonPropertyName("id")] public required Guid Id { get; init; }
}