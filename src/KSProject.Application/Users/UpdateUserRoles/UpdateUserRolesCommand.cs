using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.UpdateUserRoles;

public record UpdateUserRolesCommand(
    UpdateUserRolesRequest Payload
    ) : ICommand<UpdateUserRolesResponse>;