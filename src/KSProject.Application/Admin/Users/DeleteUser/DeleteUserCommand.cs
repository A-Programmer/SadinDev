using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.DeleteUser;

public record DeleteUserCommand(
    DeleteUserRequest Payload
    ) : ICommand<DeleteUserResponse>;
