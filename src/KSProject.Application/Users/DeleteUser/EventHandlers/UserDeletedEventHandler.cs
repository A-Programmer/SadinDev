// using KSFramework.KSMessaging.Abstraction;
// using KSProject.Domain.Aggregates.Users.Events;
// using KSProject.Domain.Contracts;
// using Microsoft.Extensions.Logging;
//
// namespace KSProject.Application.Users.DeleteUser.EventHandlers;
//
// public sealed class UserDeletedEventHandler : INotificationHandler<UserDeletedDomainEvent>
// {
//     private readonly ILogger<UserDeletedEventHandler> _logger;
//     private readonly IKSProjectUnitOfWork _uow;
//
//     public UserDeletedEventHandler(IKSProjectUnitOfWork uow,
//         ILogger<UserDeletedEventHandler> logger)
//     {
//         _uow = uow;
//         _logger = logger;
//     }
//     
//     public async Task Handle(UserDeletedDomainEvent notification, CancellationToken cancellationToken)
//     {
//         // TODO: Implement Email Sender
//         _logger.LogInformation($"The user with Id {notification.Id} and Email: {notification.Email} has been deleted.");
//     }
// }