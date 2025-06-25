using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.Register;

public sealed record RegisterCommand(
    RegisterRequest Payload
    ) : ICommand<RegisterResponse>;