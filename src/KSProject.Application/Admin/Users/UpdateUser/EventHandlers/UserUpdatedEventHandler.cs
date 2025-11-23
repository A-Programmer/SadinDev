using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users.Events;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Admin.Users.UpdateUser.EventHandlers;

public class UserUpdatedEventHandler : INotificationHandler<UserUpdatedDomainEvent>
{
	private readonly ILogger<UserUpdatedEventHandler> _logger;

	public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger)
	{
		_logger = logger;
		Console.WriteLine("UserUpdatedEventHandler initialized.");
	}

	public async Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
	{
		// TODO: This should be change to send email
		_logger.LogInformation("The user with Id {id} has been updated.", notification.Id);
	}
}
