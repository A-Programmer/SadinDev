using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Users.UpdateUserRoles;

public record UpdateUserRolesResponse(
    [property: JsonPropertyName("id")] Guid Id);
