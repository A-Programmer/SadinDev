using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.UpdateUserPermissions;
public record UpdateUserPermissionsCommand(
	UpdateUserPermissionsRequest Payload) : ICommand;