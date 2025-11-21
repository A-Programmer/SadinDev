using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Users.UpdateUserPermissions;
public sealed class UpdateUserPermissionsRequest : IInjectable
{
	[JsonProperty("id")]
	public required Guid Id { get; set; }

	[JsonProperty("permissions")]
	public required List<string> Permissions { get; set; }
}
