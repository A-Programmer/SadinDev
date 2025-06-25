using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Users.UpdateUserRoles;

public sealed class UpdateUserRolesResponse : IInjectable
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
}