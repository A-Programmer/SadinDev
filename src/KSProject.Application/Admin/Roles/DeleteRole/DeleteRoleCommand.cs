using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Roles.DeleteRole;

public record DeleteRoleCommand(
    DeleteRoleRequest Payload
    ) : ICommand<DeleteRoleResponse>;
