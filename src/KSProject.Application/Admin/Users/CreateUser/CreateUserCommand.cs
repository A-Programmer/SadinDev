using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.CreateUser;

public record CreateUserCommand(
    CreateUserRequest Payload) : ICommand<CreateUserResponse>;
