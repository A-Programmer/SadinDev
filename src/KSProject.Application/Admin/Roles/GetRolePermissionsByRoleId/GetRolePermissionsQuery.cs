using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Roles.GetRolePermissionsByRoleId;
public record GetRolePermissionsQuery(
	GetRolePermissionsByRoleIdRequest Payload) : IQuery<RolePermissionsResponse>;
