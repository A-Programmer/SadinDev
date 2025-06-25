using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    UpdateUserRequest Payload
    )
    : ICommand<UpdateUserResponse>;