using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Users.GetUserPermissionsByUserId;
public sealed class GetUserPermissionsByIdRequest : IInjectable
{
	[property: JsonProperty(nameof(id))]
	public required Guid id { get; set; }
}