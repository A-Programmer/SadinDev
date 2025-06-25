using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.CreateUser;

public record CreateUserCommand(
    CreateUserRequest Payload) : ICommand<CreateUserResponse>;