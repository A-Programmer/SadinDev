using System.Text.Json.Serialization;
using KSFramework.Contracts;

namespace KSProject.Application.Admin.Users.GetUserPermissionsByUserId;
public sealed class UserPermissionsResponse : IInjectable
{
	[JsonPropertyName("permissions")] public List<string> Permissions { get; set; } = new();
}
