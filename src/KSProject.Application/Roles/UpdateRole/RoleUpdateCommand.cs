using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Roles.UpdateRole;

public record RoleUpdateCommand(RoleUpdateRequest Payload) : ICommand<RoleUpdateResponse>;