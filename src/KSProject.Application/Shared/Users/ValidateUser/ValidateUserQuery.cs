using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Shared.Users.ValidateUser;

public sealed record ValidateUserQuery(ValidateUserQueryRequest Payload) : IQuery<ValidateUserQueryResponse?>;
