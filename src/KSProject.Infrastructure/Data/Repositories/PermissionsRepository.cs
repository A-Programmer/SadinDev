using KSFramework.GenericRepository;
using KSFramework.Pagination;
using KSProject.Domain.Aggregates.Permissions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KSProject.Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for managing Permission entities using UnitOfWork pattern.
/// </summary>
public sealed class PermissionsRepository : GenericRepository<Permission>, IPermissionsRepository
{
	private readonly DbSet<Permission> _permissions;

	public PermissionsRepository(DbContext context) : base(context)
	{
		_permissions = context.Set<Permission>();
	}

	public async Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
	}

	public async Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);
	}

	public async Task<List<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<List<Permission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.Where(p => p.Roles.Any(r => r.Id == roleId))
			.ToListAsync(cancellationToken);
	}

	public async Task<PaginatedList<Permission>> GetPagedByRoleIdAsync(
		Guid roleId,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default)
	{
		var query = _permissions.AsNoTracking().Where(p => p.Roles.Any(r => r.Id == roleId));
		if (where != null)
			query = query.Where(where);

		if (!string.IsNullOrWhiteSpace(orderBy))
			query = desc ? query.OrderByDescending(GetOrderByFunc(orderBy)) : query.OrderBy(GetOrderByFunc(orderBy));

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return new PaginatedList<Permission>(items, totalCount, pageIndex, pageSize);
	}

	public async Task<List<Permission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		return await _permissions
			.AsNoTracking()
			.Where(p => p.Users.Any(u => u.Id == userId))
			.ToListAsync(cancellationToken);
	}

	public async Task<PaginatedList<Permission>> GetPagedByUserIdAsync(
		Guid userId,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default)
	{
		var query = _permissions.AsNoTracking().Where(p => p.Users.Any(u => u.Id == userId));
		if (where != null)
			query = query.Where(where);

		if (!string.IsNullOrWhiteSpace(orderBy))
			query = desc ? query.OrderByDescending(GetOrderByFunc(orderBy)) : query.OrderBy(GetOrderByFunc(orderBy));

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return new PaginatedList<Permission>(items, totalCount, pageIndex, pageSize);
	}

	public async Task<List<Permission>> GetPermissionByUserName(string userName, CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.Where(p => p.Users.Any(u => u.UserName.ToLower() == userName.ToLower()))
			.ToListAsync(cancellationToken);
	}

	public async Task<PaginatedList<Permission>> GetPagedByUserNameAsync(
		string userName,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default)
	{
		var query = _permissions.AsNoTracking().Where(p => p.Users.Any(u => u.UserName.ToLower() == userName.ToLower()));
		if (where != null)
			query = query.Where(where);

		if (!string.IsNullOrWhiteSpace(orderBy))
			query = desc ? query.OrderByDescending(GetOrderByFunc(orderBy)) : query.OrderBy(GetOrderByFunc(orderBy));

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return new PaginatedList<Permission>(items, totalCount, pageIndex, pageSize);
	}

	public async Task<List<Permission>> GetPermissionByRoleName(string roleName, CancellationToken cancellationToken = default)
	{
		return await _permissions
			.AsNoTracking()
			.Where(p => p.Roles.Any(r => r.Name.ToLower() == roleName.ToLower()))
			.ToListAsync(cancellationToken);
	}

	public async Task<PaginatedList<Permission>> GetPagedByRoleNameAsync(
		string roleName,
		int pageIndex,
		int pageSize,
		Expression<Func<Permission, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default)
	{
		var query = _permissions.AsNoTracking().Where(p => p.Roles.Any(r => r.Name.ToLower() == roleName.ToLower()));
		if (where != null)
			query = query.Where(where);

		if (!string.IsNullOrWhiteSpace(orderBy))
			query = desc ? query.OrderByDescending(GetOrderByFunc(orderBy)) : query.OrderBy(GetOrderByFunc(orderBy));

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return new PaginatedList<Permission>(items, totalCount, pageIndex, pageSize);
	}

	public new async Task Remove(Permission entity)
	{
		ArgumentNullException.ThrowIfNull(entity, "Permission entity cannot be null.");

		await _permissions
			.Include(p => p.Roles)
			.Include(p => p.Users)
			.FirstOrDefaultAsync(_permissions => _permissions.Id == entity.Id);

		if (entity.Roles.Any() || entity.Users.Any())
		{
			entity.ClearRoles();
			entity.ClearUsers();
		}

		_permissions.Remove(entity);
	}

	/// <summary>
	/// Helper to build an order by expression dynamically.
	/// </summary>
	private static Expression<Func<Permission, object>> GetOrderByFunc(string orderBy)
	{
		return orderBy switch
		{
			nameof(Permission.Name) => p => p.Name,
			nameof(Permission.Title) => p => p.Title,
			_ => p => p.Id
		};
	}
}