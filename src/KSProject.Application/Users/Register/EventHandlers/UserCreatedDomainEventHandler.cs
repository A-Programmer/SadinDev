using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users.Events;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Users.Register.EventHandlers;

public sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
	private readonly ILogger<UserCreatedDomainEventHandler> _logger;

	public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger)
	{
		_logger = logger;
	}

	public async Task Handle(UserCreatedDomainEvent notification,
		CancellationToken cancellationToken)
	{
		// TODO: Implement Email Sender
		_logger.LogInformation($"The user with Id: {notification.Id} and Email: {notification.Email} has been created.");
	}
}