using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.User.Users.Register;

public sealed record RegisterCommand(
    RegisterRequest Payload
    ) : ICommand<RegisterResponse>;
