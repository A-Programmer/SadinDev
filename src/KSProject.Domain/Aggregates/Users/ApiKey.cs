using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KSProject.Domain.Aggregates.Users;

public sealed class ApiKey : BaseEntity, ISoftDeletable
{
    public string Key { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime? ExpirationDate { get; private set; }
    public string Scopes { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    private ApiKey(Guid id, string key, bool isActive, DateTime? expirationDate, string scopes) : base(id)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        IsActive = isActive;
        ExpirationDate = expirationDate ?? DateTime.UtcNow.AddDays(7);
        Scopes = scopes ?? string.Empty;
    }

    // Factory
    public static ApiKey Create(Guid id, Guid userId, string key, bool isActive = true, DateTime? expirationDate = null, string scopes = null)
    {
        var apiKey = new ApiKey(id, key, isActive, expirationDate, scopes);
        apiKey.SetUserId(userId);
        return apiKey;
    }

    public void Revoke()
    {
        IsActive = false;
        ExpirationDate = DateTime.UtcNow; // Immediate expire
    }

    public void ExtendExpirationDate(int days)
    {
        if (days <= 0)
            throw new ArgumentException("Days must be positive.", nameof(days));
        ExpirationDate = (ExpirationDate ?? DateTime.UtcNow).AddDays(days);
    }

    public void Update(string key, bool isActive, DateTime? expirationDate, string scopes)
    {
        Key = key ?? Key;
        IsActive = isActive;
        ExpirationDate = expirationDate ?? ExpirationDate;
        Scopes = scopes ?? Scopes;
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        UserId = userId;
    }

    public void SetUser(User user)
    {
        User = user;
    }

    // Added behavior for future: Check if expired
    public bool IsExpired()
    {
        return ExpirationDate.HasValue && ExpirationDate.Value < DateTime.UtcNow;
    }

    protected ApiKey()
    {
    }
}
public sealed class TransactionsConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasKey(x => x.Id);;
        builder.Property(x => x.Key).IsUnicode();
    }
}
