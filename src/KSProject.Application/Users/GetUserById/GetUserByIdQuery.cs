using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.GetUserById;

public record GetUserByIdQuery(
    GetUserByIdRequest Payload
    ) : IQuery<UserResponse>;