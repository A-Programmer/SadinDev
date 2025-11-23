using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.UpdateUserPermissions;
public record UpdateUserPermissionsCommand(
	UpdateUserPermissionsRequest Payload) : ICommand;
