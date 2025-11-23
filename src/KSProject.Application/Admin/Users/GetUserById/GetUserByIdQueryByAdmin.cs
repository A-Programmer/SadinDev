using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.GetUserById;

public record GetUserByIdQuery(
    Guid UserId
    ) : IQuery<UserResponse>;
