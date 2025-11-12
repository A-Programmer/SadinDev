using KSFramework.KSDomain;
using KSProject.Common.Constants.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Wallets;

public sealed class Transaction : BaseEntity, ISoftDeletable
{
    public decimal Amount { get; private set; } // مقدار: مثبت برای شارژ، منفی برای دبیت (مصرف)
    public TransactionTypes Type { get; private set; } // نوع: "Charge" برای اضافه کردن موجودی، "Usage" برای مصرف
    public string ServiceType { get; private set; } // نوع سرویس: e.g., "Slider", "Storage", "Blog"
    public string MetricType { get; private set; } // نوع متریک: e.g., "Slides", "GB", "Posts"
    public decimal MetricValue { get; private set; } // مقدار متریک مصرف‌شده: e.g., 5 برای 5 اسلاید
    public DateTime TransactionDateTime { get; private set; } // زمان تراکنش
    public string? MetricDetails { get; private set; }
    public Guid WalletId { get; private set; }
    public Wallet Wallet { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    private Transaction(Guid id,
        decimal amount,
        TransactionTypes transactionType,
        string serviceType,
        string metricType,
        decimal metricValue,
        string metricDetails
        ) : base(id)
    {
        Amount = amount;
        Type = transactionType;
        ServiceType = serviceType;
        MetricType = metricType;
        MetricValue = metricValue;
        MetricDetails = metricDetails;
    }
    
    private Transaction(Guid id, Guid walletId, decimal amount, TransactionTypes type, string serviceType, string metricType, decimal metricValue) : base(id)
    {
        if (walletId == Guid.Empty)
            throw new ArgumentException("WalletId cannot be empty.", nameof(walletId));
        WalletId = walletId;

        Amount = amount;
        Type = type;
        ServiceType = serviceType ?? string.Empty;
        MetricType = metricType ?? string.Empty;
        MetricValue = metricValue >= 0 ? metricValue : throw new ArgumentException("MetricValue cannot be negative.");
    }

    // Factory method
    public static Transaction Create(Guid id, Guid walletId, decimal amount, TransactionTypes type, string serviceType = null, string metricType = null, decimal metricValue = 0)
    {
        return new Transaction(id, walletId, amount, type, serviceType, metricType, metricValue);
    }

    // Behavioral methods
    public void UpdateAmount(decimal newAmount)
    {
        if (newAmount == 0)
            throw new ArgumentException("Amount cannot be zero.", nameof(newAmount));
        Amount = newAmount;
    }

    public bool IsUsageTransaction()
    {
        return Type == TransactionTypes.Usage;
    }
}

public sealed class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.Type).HasConversion<string>();
    }
}
