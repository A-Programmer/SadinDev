using System.Linq.Expressions;
using KSFramework.GenericRepository;
using KSFramework.Pagination;

namespace KSProject.Domain.Aggregates.Users;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> FindUserWithRolesAsync(Guid id,
        CancellationToken cancellationToken = default);
    
    Task<User?> FindUserByUserNameAsync(string userName,
        CancellationToken cancellationToken = default);

    Task<User?> FindUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);

    Task<User?> FindUserByPhoneNumber(string phoneNumber,
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
}