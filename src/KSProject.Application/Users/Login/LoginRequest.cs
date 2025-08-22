using KSFramework.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KSProject.Application.Users.Login;

public sealed class LoginRequest : IInjectable
{
	[Required]
	public required string UserName { get; init; }

    [Required]
	public required string Password { get; init; }

	[JsonIgnore]
	public string IpAddress { get; set; } = string.Empty;
}