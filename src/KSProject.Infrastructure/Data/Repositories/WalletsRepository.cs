using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSProject.Domain.Aggregates.Wallets;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Infrastructure.Data.Repositories;

public class WalletsRepository : GenericRepository<Wallet>, IWalletsRepository
{
    private readonly DbSet<Wallet> _wallets;
    public WalletsRepository(KSProjectDbContext context) : base(context)
    {
        _wallets = context.Set<Wallet>();
    }
    
    public async Task<Wallet> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _wallets
            .Include(w => w.Transactions)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public async Task<Wallet> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _wallets
            .Include(w => w.Transactions)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
    }

    public async Task AddTransactionAsync(Guid walletId, Transaction transaction, CancellationToken cancellationToken = default)
    {
        Wallet wallet = await GetByIdAsync(walletId, cancellationToken);
        if (wallet == null)
        {
            throw new InvalidOperationException("Wallet not found.");
        }

        wallet.AddTransaction(transaction);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default)
    {
        Wallet wallet = await GetByIdAsync(walletId, cancellationToken);
        return wallet?.Transactions ?? new List<Transaction>();
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid walletId, Guid transactionId, CancellationToken cancellationToken = default)
    {
        var wallet = await GetByIdAsync(walletId, cancellationToken);
        return wallet?.Transactions.FirstOrDefault(t => t.Id == transactionId && !t.IsDeleted);
    }
}
