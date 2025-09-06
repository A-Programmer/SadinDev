using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.GetUserPermissionsByUserId;
public sealed class GetUserPermissionsByIdQueryHandler
	: IQueryHandler<GetUserPermissionsByIdQuery, UserPermissionsResponse>
{
	private readonly IKSProjectUnitOfWork _uow;
	public GetUserPermissionsByIdQueryHandler(IKSProjectUnitOfWork uow)
	{
		_uow = uow;
	}

	public async Task<UserPermissionsResponse> Handle(GetUserPermissionsByIdQuery request, CancellationToken cancellationToken)
	{
		User user = await _uow.Users.GetUserAndPermissionsAsNoTrackingAsync(request.Payload.id, cancellationToken)
			?? throw new Exception("User not found");

		return new UserPermissionsResponse
		{
			Permissions = user.Permissions
				.Select(p => p.Name)
				.Distinct()
				.ToList()
		};
	}
}
