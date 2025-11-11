using KSFramework.Contracts;
using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Wallets.Events
{
    public sealed class WalletCreatedEvent : IDomainEvent, IInjectable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
