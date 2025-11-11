using KSFramework.GenericRepository;

namespace KSProject.Domain.Aggregates.Wallets;

public interface IWalletsRepository : IRepository<Wallet>
{
    Task<Wallet> GetByIdAsync(Guid id);
    Task<Wallet> GetByUserIdAsync(Guid userId);
    
    Task AddTransactionAsync(Guid walletId, Transaction transaction);
    Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(Guid walletId);
}
