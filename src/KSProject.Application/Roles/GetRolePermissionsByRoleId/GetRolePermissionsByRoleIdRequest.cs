using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Roles.GetRolePermissionsByRoleId;
public record GetRolePermissionsByRoleIdRequest : IInjectable
{
	[property: JsonProperty(nameof(id))]
	public required Guid id { get; set; }
}