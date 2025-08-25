using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.GetUserPermissionsByUserId;
public record GetUserPermissionsByIdQuery(
	GetUserPermissionsByIdRequest Payload) : IQuery<UserPermissionsResponse>;