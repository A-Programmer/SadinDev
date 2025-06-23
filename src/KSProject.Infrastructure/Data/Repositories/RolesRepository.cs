using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Infrastructure.Data.Repositories;

public sealed class RolesRepository : GenericRepository<Role>, IRolesRepository
{
    private readonly DbSet<Role> _roles;
    public RolesRepository(DbContext context) : base(context)
    {
        _roles = context.Set<Role>();
    }

    public async Task<Role?> GetByRoleName(string roleName,
        CancellationToken cancellationToken = default)
    {
        return await _roles
            .FirstOrDefaultAsync(x => x.Name.ToLower() == roleName,
                cancellationToken);
    }

    public async Task<Role?> GetRoleByIdIncludingUsersAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _roles
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == id,
                cancellationToken);
    }

    public async Task<List<Role>> GetRolesByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await _roles
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(cancellationToken);
    }
}