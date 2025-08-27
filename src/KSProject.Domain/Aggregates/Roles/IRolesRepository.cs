using KSFramework.GenericRepository;

namespace KSProject.Domain.Aggregates.Roles;

public interface IRolesRepository : IRepository<Role>
{
	Task<Role?> GetByRoleName(string roleName,
		CancellationToken cancellationToken = default);

	Task<Role?> GetRoleByIdIncludingUsersAsync(Guid id,
		CancellationToken cancellationToken = default);

	Task<List<Role>> GetRolesByIdsAsync(IEnumerable<Guid> ids,
		CancellationToken cancellationToken = default);

	Task<Role?> GetRoleAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default);
}