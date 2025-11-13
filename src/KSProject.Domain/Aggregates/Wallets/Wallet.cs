using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSProject.Common.Constants.Enums;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Wallets.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Wallets;

public sealed class Wallet : BaseEntity, IAggregateRoot, ISoftDeletable
{
    private Wallet(Guid id,
        Guid userId,
        decimal balance) : base(id)
    {
        Balance = balance;
        UserId = userId;
    }
    public decimal Balance { get; private set; } = 0.0m;
    public Guid UserId { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    
    private List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    
    public static Wallet Create(Guid id, Guid userId, decimal initialBalance = 0.0m)
    {
        var wallet = new Wallet(id, userId, initialBalance);
        wallet.AddDomainEvent(new WalletCreatedEvent { Id = id, UserId = userId, InitialBalance = initialBalance });
        return wallet;
    }

    // Behavioral methods
    public void UpdateBalance(decimal amount, TransactionTypes type, string serviceType = null, string metricType = null, decimal metricValue = 0)
    {
        if (amount == 0)
            throw new ArgumentException("Amount cannot be zero.", nameof(amount));

        decimal newBalance = Balance + amount;
        if (newBalance < 0)
            throw new InvalidOperationException("Balance cannot go negative.");

        Balance = newBalance;

        // Add transaction automatically
        var transaction = Transaction.Create(Guid.NewGuid(), Id, amount, type, serviceType, metricType, metricValue);
        AddTransaction(transaction);

        AddDomainEvent(new BalanceChangedEvent { WalletId = Id, NewBalance = Balance, Amount = amount });
    }
    
    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }
    
    public bool CheckSufficientBalance(decimal requiredAmount)
    {
        return Balance >= requiredAmount;
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction.WalletId != Id)
            throw new InvalidOperationException("Transaction must belong to this wallet.");

        _transactions.Add(transaction);
    }

    public void RemoveTransaction(Guid transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction != null)
        {
            _transactions.Remove(transaction);
        }
    }

    public void ClearTransactions()
    {
        _transactions.Clear();
    }

    public decimal GetTotalUsageByService(string serviceType)
    {
        return _transactions
            .Where(t => t.Type == TransactionTypes.Usage && t.ServiceType == serviceType)
            .Sum(t => t.Amount); // منفی برای usage
    }

    // For future: Transfer to another wallet
    public void TransferTo(Wallet targetWallet, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be positive.", nameof(amount));
        if (!CheckSufficientBalance(amount))
            throw new InvalidOperationException("Insufficient balance for transfer.");

        UpdateBalance(-amount, TransactionTypes.Transfer);
        targetWallet.UpdateBalance(amount, TransactionTypes.Transfer);
    }

    protected Wallet() { }
}

public sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);
        
        
        
        builder.HasIndex(x => x.UserId)
            .IsUnique();
        
        builder.HasMany(w => w.Transactions)
            .WithOne(w => w.Wallet)
            .HasForeignKey(w => w.WalletId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
