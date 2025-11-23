using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users.Events;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.User.Users.Register.EventHandlers;

public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;

    public UserRegisteredDomainEventHandler(ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        // TODO: Implement Email Sender
        _logger.LogInformation($"\n\n\n\n\n\nThe user with Id: {notification.Id} and Email: {notification.Email} has been registered.\n\n\n\n\n\n");
    }
}
