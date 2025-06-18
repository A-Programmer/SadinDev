using KSFramework.KSDomain;

namespace KSProject.Domain.Aggregates.Users.Events;

public record UserCreatedDomainEvent(Guid Id) : IDomainEvent
{
    public DateTime OccurredOn { get; set; }
}