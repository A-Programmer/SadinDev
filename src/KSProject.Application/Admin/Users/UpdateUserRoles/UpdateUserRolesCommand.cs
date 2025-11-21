using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.UpdateUserRoles;

public record UpdateUserRolesCommand(
    UpdateUserRolesRequest Payload
    ) : ICommand<UpdateUserRolesResponse>;
