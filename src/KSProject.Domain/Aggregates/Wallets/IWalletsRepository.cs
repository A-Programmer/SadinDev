using System.Linq.Expressions;
using KSFramework.GenericRepository;
using KSFramework.Pagination;

namespace KSProject.Domain.Aggregates.Wallets;

public interface IWalletsRepository : IRepository<Wallet>
{
    Task<Wallet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Wallet> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default);
    PaginatedList<Transaction> GetTransactionsByUserIdAsync(Guid userId,
        int pageIndex,
        int pageSize,
        Expression<Func<Transaction, bool>>? where = null,
        string orderBy = "",
        bool desc = false);
    
}
