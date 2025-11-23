using KSFramework.Contracts;
using Newtonsoft.Json;

namespace KSProject.Application.Admin.Users.UpdateUser;

public sealed class UpdateUserRequest : IInjectable
{
	[JsonProperty("id")]
	public required Guid Id { get; init; }

	[JsonProperty("userName")]
	public required string UserName { get; init; }

	[JsonProperty("email")]
	public required string Email { get; init; }

	[JsonProperty("phoneNumber")]
	public required string PhoneNumber { get; init; }

	[JsonProperty("active")]
	public required bool Active { get; init; }

	[JsonProperty("roles")]
	public required string[] Roles { get; init; }
}
