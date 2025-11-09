using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Users;

public sealed class Wallet : BaseEntity, ISoftDeletable
{
    private Wallet(Guid id,
        decimal balance) : base(id)
    {
        Balance = balance;
    }
    public decimal Balance { get; set; } = 0.0m;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    
    public void UpdateBalance(decimal balance)
    {
        this.Balance = balance;
    }
    
    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public static Wallet Create(Guid id, Guid userId, decimal balance)
    {
        Wallet wallet = new(id, balance)
        {
            UserId = userId
        };
        
        return wallet;
    }

    protected Wallet() { }
}
