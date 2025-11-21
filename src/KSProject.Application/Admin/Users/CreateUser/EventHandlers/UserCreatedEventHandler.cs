using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users.Events;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Admin.Users.CreateUser.EventHandlers;

public sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
	private readonly ILogger<UserCreatedEventHandler> _logger;

	public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
	{
		_logger = logger;
	}

	public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
	{
		// TODO: Implement Email Sender
		_logger.LogInformation($"The user with Id: {notification.Id} and Email: {notification.Email} has been created.");
	}
}
