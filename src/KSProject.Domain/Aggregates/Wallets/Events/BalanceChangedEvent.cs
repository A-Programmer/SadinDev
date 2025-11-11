using KSFramework.Contracts;
using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Wallets.Events
{
    public sealed class BalanceChangedEvent : IDomainEvent, IInjectable
    {
        public Guid WalletId { get; set; }
        public decimal NewBalance { get; set; }
        public decimal Amount { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
