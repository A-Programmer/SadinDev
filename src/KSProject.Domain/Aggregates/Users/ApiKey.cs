using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Users;

public sealed class ApiKey : BaseEntity, ISoftDeletable
{
    public string Key { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset? ExpirationDate { get; set; }
    public string Scopes { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    private ApiKey(Guid id,
        string key,
        bool isActive,
        DateTimeOffset? expirationDate,
        string scopes) : base(id)
    {
        Key = key;
        IsActive = isActive;
        ExpirationDate = expirationDate ?? DateTimeOffset.UtcNow.AddDays(7);
        Scopes = scopes;
    }

    public void RevokeApiKey(string key)
    {
        Key = key;
    }

    public void ExtendExpirationDate(int days)
    {
        ExpirationDate ??= DateTimeOffset.UtcNow.AddDays(days);
        
        if (ExpirationDate != null && ExpirationDate.Value.Date < DateTimeOffset.UtcNow)
        {
            ExpirationDate = DateTimeOffset.UtcNow.AddDays(days);
        }
        else if (ExpirationDate != null && ExpirationDate.Value.Date >= DateTimeOffset.UtcNow)
        {
            ExpirationDate = ExpirationDate?.AddDays(days);
        }
    }

    public void Update(string key,
        bool isActive,
        DateTimeOffset expirationDate,
        string scopes)
    {
        Key = key;
        IsActive = isActive;
        ExpirationDate = expirationDate;
        Scopes = scopes;
    }
    
    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public static ApiKey Create(Guid id,
        Guid userId,
        string key,
        bool isActive,
        DateTimeOffset? expirationDate,
        string scopes)
    {
        ApiKey apiKey = new(id, key, isActive, expirationDate, scopes)
        {
            UserId = userId
        };
        
        return apiKey;
    }

    protected ApiKey()
    {
    }
}
