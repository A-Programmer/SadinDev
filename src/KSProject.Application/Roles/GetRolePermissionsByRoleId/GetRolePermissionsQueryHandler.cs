using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Roles.GetRolePermissionsByRoleId;
public sealed class GetRolePermissionsQueryHandler : IQueryHandler<GetRolePermissionsQuery, RolePermissionsResponse>
{
	private readonly IKSProjectUnitOfWork _uow;
	public GetRolePermissionsQueryHandler(IKSProjectUnitOfWork uow)
	{
		_uow = uow;
	}

	public async Task<RolePermissionsResponse> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
	{
		Role? role = await _uow.Roles.GetRoleAndPermissionsAsync(request.Payload.id, cancellationToken) ?? throw new KSNotFoundException("The role with provided id could not be found.");

		return new RolePermissionsResponse
		{
			Permissions = role.Permissions
			.Select(x => x.Name)
			.Distinct()
			.ToList()
		};
	}
}
