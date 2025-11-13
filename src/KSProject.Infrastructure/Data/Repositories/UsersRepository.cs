using KSFramework.GenericRepository;
using KSFramework.Pagination;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KSProject.Infrastructure.Data.Repositories;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
	private readonly DbSet<User> _users;
	public UsersRepository(KSProjectDbContext context) : base(context)
	{
		_users = context.Set<User>();
	}

	public async Task<User?> FindUserWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Roles)
			.Include(u => u.UserTokens)
			.Include(u => u.UserSecurityStamps)
			.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
	}

	public async Task<User?> FindUserByUserNameAsync(string userName,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Roles)
			.Include(u => u.UserTokens)
			.Include(u => u.UserSecurityStamps)
			.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName, cancellationToken);
	}

	public async Task<User?> FindUserByEmailAsync(string email,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Roles)
			.Include(u => u.UserTokens)
			.Include(u => u.UserSecurityStamps)
			.FirstOrDefaultAsync(x => x.Email.ToLower() == email, cancellationToken);
	}

	public async Task<User?> FindUserByPhoneNumberAsync(string phoneNumber,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Roles)
			.Include(u => u.UserTokens)
			.Include(u => u.UserSecurityStamps)
			.FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == phoneNumber, cancellationToken);
	}

	public async Task<bool> IsUserNameInUseAsync(Guid id,
		string userName,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.FirstOrDefaultAsync(x => x.Id != id &&
									  x.UserName.ToLower() == userName,
				cancellationToken) is not null;
	}

	public async Task<bool> IsEmailInUseAsync(Guid id,
		string email,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.FirstOrDefaultAsync(x => x.Id != id &&
									  x.Email.ToLower() == email,
				cancellationToken) is not null;
	}

	public async Task<bool> IsPhoneNumberInUseAsync(Guid id,
		string phoneNumber,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.FirstOrDefaultAsync(x => x.Id != id &&
									  x.PhoneNumber.ToLower() == phoneNumber,
				cancellationToken) is not null;
	}

	public async Task<PaginatedList<User>> GetPaginatedUsersWithRolesAsync(int pageIndex,
		int pageSize,
		Expression<Func<User, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default)
	{
		return await PaginatedList<User>
			.CreateAsync(
				_users.Include(u => u.Roles).AsQueryable(),
				pageIndex,
				pageSize,
				where,
				orderBy,
				desc,
				cancellationToken);
	}

	public async Task<User?> GetByIdIncludingRolesAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Permissions)
			.Include(u => u.Roles)
			.ThenInclude(r => r.Permissions)
			.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
	}

	public async Task<User?> FindUserIncludingRolesAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Permissions)
			.Include(u => u.Roles)
			.ThenInclude(u => u.Permissions)
			.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
	}

	public async Task<User?> GetUserAndPermissionsAsNoTrackingAsync(Guid id,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Permissions)
			.AsNoTracking()
			.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
	}

	public async Task<User?> GetUserAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default)
	{
		return await _users
			.Include(u => u.Permissions)
			.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
	}




    public async Task<User> GetUserByIdIncludingApiKeysAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _users
            .Include(u => u.ApiKeys)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<ApiKey?> GetApiKeyByKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _users
            .SelectMany(u => u.ApiKeys)
            .FirstOrDefaultAsync(ak => ak.Key == key && ak.IsActive, cancellationToken);
    }

    public async Task<ApiKey> GenerateApiKeyForUserAsync(Guid userId, string scopes = null, CancellationToken cancellationToken = default)
    {
        User user = await _users
            .Include(u => u.ApiKeys)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var newApiKey = ApiKey.Create(Guid.NewGuid(), userId, Guid.NewGuid().ToString("N"), true, null, scopes ?? "");
        user.AddApiKey(newApiKey);
        return newApiKey;
    }

    public async Task AddApiKeyToUserAsync(Guid userId, ApiKey apiKey, CancellationToken cancellationToken = default)
    {
        User user = await _users
            .Include(u => u.ApiKeys)
            .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        user.AddApiKey(apiKey);
    }

    public async Task<IEnumerable<ApiKey>> GetApiKeysByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        User user = await _users
            .Include(u => u.ApiKeys) // load collection
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        return user.ApiKeys.Where(ak => !ak.IsDeleted);
    }

    public async Task RevokeApiKeyAsync(Guid userId, Guid apiKeyId, CancellationToken cancellationToken = default)
    {
        User user = await _users
            .Include(u => u.ApiKeys)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var apiKey = user.ApiKeys.FirstOrDefault(ak => ak.Id == apiKeyId && !ak.IsDeleted);
        if (apiKey != null)
        {
            apiKey.Revoke();
        }
    }
}
