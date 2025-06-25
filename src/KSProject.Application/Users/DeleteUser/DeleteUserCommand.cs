using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.DeleteUser;

public record DeleteUserCommand(
    DeleteUserRequest Payload
    ) : ICommand<DeleteUserResponse>;