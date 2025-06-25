using System.Security.Authentication;
using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.Utilities;
using KSProject.Common.Constants.Messages;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Contracts;

// TODO: The login should return Access Token and Refresh Token
namespace KSProject.Application.Users.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IKSProjectUnitOfWork _uow;
    private readonly IJwtService _jwtService; 

    public LoginCommandHandler(IKSProjectUnitOfWork uow,
        IJwtService jwtService)
    {
        _uow = uow;
        _jwtService = jwtService;
    }
    
    public async Task<LoginResponse> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _uow.Users.FindUserByUserNameAsync(request.Payload.UserName,
            cancellationToken);
        
        if (user is null)
            throw new KSNotFoundException(request.Payload.UserName);

        string hashedPassword = SecurityHelper.GetSha256Hash(request.Payload.Password);
        if (user.HashedPassword != hashedPassword)
            throw new AuthenticationException(GeneralMessages.WrongUserNameOrPassword);

        return new LoginResponse
        {
            Token = _jwtService.GenerateToken(user)
        };
    }
}