using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.Users.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IKSProjectUnitOfWork uow,
        ILogger<RegisterCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.Password);

        // TODO: Of course, the active status should be changed in the future, at least, it should be activated after email/phone confirmation
        User user = User.Register(Guid.NewGuid(), request.Payload.UserName, hashedPassword, request.Payload.Email, request.Payload.PhoneNumber, active: true);

        Role? role = await _uow.Roles.GetByRoleName("User", cancellationToken);
        if (role == null)
            throw new KSNotFoundException("Role not found");
        
        
        user.AddWallet(Wallet.Create(Guid.NewGuid(), user.Id, 0));
        user.AddProfile(UserProfile.Create(Guid.NewGuid(),
            user.Id,
            request.Payload.FirstName ?? "",
            request.Payload.LastName ?? "",
            "/profile_images/default.png",
            request.Payload.AboutMe ?? "",
            request.Payload.BirthDateUtc));

        // UserProfile userProfile = UserProfile.Create(Guid.NewGuid(),
        //     user.Id,
        //     request.Payload.FirstName ?? "",
        //     request.Payload.LastName ?? "",
        //     "/profile_images/default.png",
        //     request.Payload.AboutMe ?? "",
        //     request.Payload.BirthDateUtc);
        //
        // user.AddProfile(userProfile);

        try
        {
            user.AssignRoles(new[] { role });

            await _uow.Users.AddAsync(user, cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);

            return new RegisterResponse
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
}
