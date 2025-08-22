using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.ValidateUser;

public sealed record ValidateUserQuery(ValidateUserQueryRequest Payload) : IQuery<ValidateUserQueryResponse?>;
