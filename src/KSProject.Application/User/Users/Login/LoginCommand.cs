using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.User.Users.Login;

public sealed record LoginCommand(
	LoginRequest Payload
	) : ICommand<LoginResponse>;
