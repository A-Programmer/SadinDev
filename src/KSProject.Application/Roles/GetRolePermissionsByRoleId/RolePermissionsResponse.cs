using KSFramework.Contracts;
using System.Text.Json.Serialization;

namespace KSProject.Application.Roles.GetRolePermissionsByRoleId;
public sealed class RolePermissionsResponse : IInjectable
{
	[JsonPropertyName("permissions")] public List<string> Permissions { get; set; } = new();
}
