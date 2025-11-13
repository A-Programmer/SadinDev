using KSFramework.GenericRepository;
using KSFramework.Pagination;
using System.Linq.Expressions;

namespace KSProject.Domain.Aggregates.Users;

public interface IUsersRepository : IRepository<User>
{
	Task<User?> FindUserWithRolesAsync(Guid id,
		CancellationToken cancellationToken = default);

	Task<User?> FindUserIncludingRolesAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default);

	Task<User?> FindUserByUserNameAsync(string userName,
		CancellationToken cancellationToken = default);

	Task<User?> FindUserByEmailAsync(string email,
		CancellationToken cancellationToken = default);

	Task<User?> FindUserByPhoneNumberAsync(string phoneNumber,
		CancellationToken cancellationToken = default);

	Task<bool> IsUserNameInUseAsync(Guid id,
		string userName,
		CancellationToken cancellationToken = default);

	Task<bool> IsEmailInUseAsync(Guid id,
		string email,
		CancellationToken cancellationToken = default);

	Task<bool> IsPhoneNumberInUseAsync(Guid id,
		string phoneNumber,
		CancellationToken cancellationToken = default);

	Task<PaginatedList<User>> GetPaginatedUsersWithRolesAsync(int pageIndex,
		int pageSize,
		Expression<Func<User, bool>>? where = null,
		string orderBy = "",
		bool desc = false,
		CancellationToken cancellationToken = default);

	Task<User?> GetByIdIncludingRolesAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default);

	Task<User?> GetUserAndPermissionsAsNoTrackingAsync(Guid id,
		CancellationToken cancellationToken = default);

	Task<User?> GetUserAndPermissionsAsync(Guid id,
		CancellationToken cancellationToken = default);
    
    
    Task<User> GetUserByIdIncludingApiKeysAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiKey> GetApiKeyByKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<ApiKey> GenerateApiKeyForUserAsync(Guid userId, string scopes = null, CancellationToken cancellationToken = default);
    Task AddApiKeyToUserAsync(Guid userId, ApiKey apiKey, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApiKey>> GetApiKeysByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task RevokeApiKeyAsync(Guid userId, Guid apiKeyId, CancellationToken cancellationToken = default);
}
