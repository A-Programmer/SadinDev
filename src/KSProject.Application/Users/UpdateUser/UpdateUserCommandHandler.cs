using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler
    : ICommandHandler<UpdateUserCommand,
        UpdateUserResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public UpdateUserCommandHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User user = (await _uow.Users.FindUserWithRolesAsync(request.Payload.Id,
            cancellationToken)) ??
                     throw new KSNotFoundException(request.Payload.UserName);

        if (await AreInformationInUse(request, cancellationToken))
            throw new KsDuplicatedUserException("A user with the same information exist.");

        user.Update(
            request.Payload.UserName,
            request.Payload.Email,
            request.Payload.PhoneNumber);

        List<Role> roles = new();
        
        foreach (string roleName in request.Payload.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName,
                cancellationToken) ?? throw new KSNotFoundException(roleName);
            
            roles.Add(role);
        }
        
        user.ClearRoles();
        user.AssignRoles(roles);

        await _uow.SaveChangesAsync();

        return new UpdateUserResponse
        {
            Id = request.Payload.Id
        };
    }

    private async Task<bool> AreInformationInUse(UpdateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        return await _uow
            .Users
            .IsEmailInUseAsync(
                request.Payload.Id,
                request.Payload.Email,
                cancellationToken) ||
               
        await _uow
                .Users.IsPhoneNumberInUseAsync(
                    request.Payload.Id,
                    request.Payload.PhoneNumber,
                    cancellationToken) ||

            await _uow
                .Users.IsUserNameInUseAsync(
                    request.Payload.Id,
                    request.Payload.UserName,
                    cancellationToken);
    }
}