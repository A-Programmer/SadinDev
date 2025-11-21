using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace KSProject.Application.User.Users.ResetPassword;

// TODO: Reset Password should be use Token, implement the token for reset password
public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;
    
    public ResetPasswordCommandHandler(IKSProjectUnitOfWork uow, ILogger<ResetPasswordCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Users.User? user = await _uow.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new KSNotFoundException(request.Id.ToString());

        string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.NewPassword);
        
        user.UpdatePassword(hashedPassword);

        await _uow.SaveChangesAsync();

        return new ResetPasswordResponse()
        {
            Result = true
        };
    }
}
