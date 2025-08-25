using KSFramework.Contracts;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.GetUserPermissionsByUserId;
public sealed class UserPermissionsResponse : IInjectable
{
	[JsonPropertyName("permissions")] public List<string> Permissions { get; set; } = new();
}
