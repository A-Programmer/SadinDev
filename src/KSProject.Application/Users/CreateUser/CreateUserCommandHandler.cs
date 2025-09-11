using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Common.Exceptions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IKSProjectUnitOfWork uow,
        ILogger<CreateUserCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await CheckIfUserExist(request, cancellationToken))
            throw new KsDuplicatedUserException("A user with the same provided information is exist.");

        string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.Password);

        User user = User.Create(
            Guid.NewGuid(),
            request.Payload.UserName,
            hashedPassword,
            request.Payload.Email,
            request.Payload.PhoneNumber);

        List<Role> roles = new();

        foreach (string roleName in request.Payload.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName, cancellationToken);

            if (role == null)
                throw new KSNotFoundException("Role not found");

            roles.Add(role);
        }

        UserProfile userProfile = UserProfile.Create(Guid.NewGuid(),
            "",
            "",
            "/profile_images/default.png",
            "",
            null);

        user.AddProfile(userProfile);

        try
        {
            user.AssignRoles(roles);

            await _uow.Users.AddAsync(user, cancellationToken);

            await _uow.SaveChangesAsync();

            return new CreateUserResponse()
            {
                Id = user.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on adding user: {ErrorMessage}", ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
            throw;
        }
    }

    private async Task<bool> CheckIfUserExist(CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        return
            (await _uow.Users.FindUserByUserNameAsync(request.Payload.UserName, cancellationToken) is not null) ||
            (await _uow.Users.FindUserByEmailAsync(request.Payload.Email, cancellationToken) is not null) ||
            (await _uow.Users.FindUserByPhoneNumberAsync(request.Payload.PhoneNumber, cancellationToken) is not null);
    }
}
