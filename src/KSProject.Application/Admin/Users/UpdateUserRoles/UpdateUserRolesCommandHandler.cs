using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Admin.Users.UpdateUserRoles;

public sealed class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand, UpdateUserRolesResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ILogger<UpdateUserRolesCommandHandler> _logger;

    public UpdateUserRolesCommandHandler(IKSProjectUnitOfWork uow, ILogger<UpdateUserRolesCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UpdateUserRolesResponse> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Users.User user = (await _uow.Users.FindUserWithRolesAsync(request.Payload.Id,
                                                cancellationToken)) ??
                                            throw new KSNotFoundException(request.Payload.Id.ToString());
        
        List<Role> roles = new();

        foreach (string roleName in request.Payload.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName, cancellationToken);
            
            if (role == null)
                throw new KSNotFoundException("Role not found");
            
            roles.Add(role);
        }
        
        try
        {
            user.ClearRoles();
            
            user.AssignRoles(roles);
            
            await _uow.SaveChangesAsync(cancellationToken);

            return new UpdateUserRolesResponse(user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on updating user roles: {ErrorMessage}", ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
            throw;
        }
    }
}
