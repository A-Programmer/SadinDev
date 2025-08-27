using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Roles.GetRolePermissionsByRoleId;
public record GetRolePermissionsQuery(
	GetRolePermissionsByRoleIdRequest Payload) : IQuery<RolePermissionsResponse>;