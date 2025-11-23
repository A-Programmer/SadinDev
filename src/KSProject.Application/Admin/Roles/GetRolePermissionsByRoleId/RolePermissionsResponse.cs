using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Roles.GetRolePermissionsByRoleId;
public sealed class RolePermissionsResponse : IInjectable
{
	[JsonPropertyName("permissions")] public List<string> Permissions { get; set; } = new();
}
