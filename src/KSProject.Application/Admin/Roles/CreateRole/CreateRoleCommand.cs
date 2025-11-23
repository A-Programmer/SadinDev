using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Roles.CreateRole;

public record CreateRoleCommand(CreateRoleRequest Payload) : ICommand<CreateRoleResponse>, IInjectable;
