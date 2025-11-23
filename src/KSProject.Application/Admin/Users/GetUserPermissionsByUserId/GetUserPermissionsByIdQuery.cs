using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.GetUserPermissionsByUserId;
public record GetUserPermissionsByIdQuery(
	GetUserPermissionsByIdRequest Payload) : IQuery<UserPermissionsResponse>;
