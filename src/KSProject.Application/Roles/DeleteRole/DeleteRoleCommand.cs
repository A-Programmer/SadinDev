using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Roles.DeleteRole;

public record DeleteRoleCommand(
    DeleteRoleRequest Payload
    ) : ICommand<DeleteRoleResponse>;