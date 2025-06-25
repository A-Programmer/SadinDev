using KSFramework.Exceptions;
using KSFramework.GenericRepository;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IKSProjectUnitOfWork uow,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _uow.Users
                        .FindUserWithRolesAsync(request.Payload.id, cancellationToken)  ??
                    throw new KSNotFoundException("The user could not be found.");
        
        // remove children
        user.ClearRoles();
        
        _uow.Users.Remove(user);
        
        await _uow.SaveChangesAsync();

        return new DeleteUserResponse()
        {
            Id = request.Payload.id
        };
    }
}