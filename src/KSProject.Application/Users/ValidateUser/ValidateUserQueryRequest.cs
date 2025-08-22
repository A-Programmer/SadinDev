using KSFramework.Contracts;

namespace KSProject.Application.Users.ValidateUser;
public sealed class ValidateUserQueryRequest : IInjectable
{
	public required string UserName { get; set; }
	public required string Password { get; set; }
}
