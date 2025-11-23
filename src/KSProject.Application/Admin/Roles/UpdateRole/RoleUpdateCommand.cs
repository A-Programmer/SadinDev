using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Roles.UpdateRole;

public record RoleUpdateCommand(RoleUpdateRequest Payload) : ICommand<RoleUpdateResponse>;
