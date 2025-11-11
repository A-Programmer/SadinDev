using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Wallets;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Infrastructure.Data.Repositories;

public class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
{
    private readonly DbSet<Wallet> _wallets;
    public WalletsRepository(DbContext context) : base(context)
    {
        _wallets = context.Set<Wallet>();
    }
    
    public async Task<Wallet> GetByIdAsync(Guid id)
    {
        return await _wallets
            .Include(w => w.Transactions)
            .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted);
    }

    public async Task<Wallet> GetByUserIdAsync(Guid userId)
    {
        return await _wallets.Include(w => w.Transactions)
            .FirstOrDefaultAsync(w => w.UserId == userId);
    }

    public async Task AddTransactionAsync(Guid walletId, Transaction transaction)
    {
        Wallet wallet = await GetByIdAsync(walletId);
        if (wallet == null)
        {
            throw new InvalidOperationException("Wallet not found.");
        }

        wallet.AddTransaction(transaction);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(Guid walletId)
    {
        Wallet wallet = await GetByIdAsync(walletId);
        return wallet?.Transactions ?? new List<Transaction>();
    }
}
