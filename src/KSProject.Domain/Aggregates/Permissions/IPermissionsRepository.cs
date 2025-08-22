using KSFramework.GenericRepository;
using KSFramework.Pagination;
using System.Linq.Expressions;

namespace KSProject.Domain.Aggregates.Permissions;

public interface IPermissionsRepository : IRepository<Permission>
{
	Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

	Task<List<Permission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);

	Task<PaginatedList<Permission>> GetPagedByRoleIdAsync(
		Guid roleId,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default);

	Task<List<Permission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

	Task<PaginatedList<Permission>> GetPagedByUserIdAsync(
		Guid userId,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default);

	Task<List<Permission>> GetPermissionByUserName(string userName, CancellationToken cancellationToken = default);

	Task<PaginatedList<Permission>> GetPagedByUserNameAsync(
		string userName,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default);

	Task<List<Permission>> GetPermissionByRoleName(string roleName, CancellationToken cancellationToken = default);
}
