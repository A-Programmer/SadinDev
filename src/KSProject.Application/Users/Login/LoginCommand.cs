using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.Login;

public sealed record LoginCommand(
    LoginRequest Payload
    ) : ICommand<LoginResponse>;