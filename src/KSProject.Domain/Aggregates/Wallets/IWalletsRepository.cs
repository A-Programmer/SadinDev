using KSFramework.GenericRepository;

namespace KSProject.Domain.Aggregates.Wallets;

public interface IWalletsRepository : IRepository<Wallet>
{
    Task<Wallet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Wallet> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default);
    
}
