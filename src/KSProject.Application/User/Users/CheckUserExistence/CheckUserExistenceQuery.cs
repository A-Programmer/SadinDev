using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.User.Users.CheckUserExistence;

public sealed record CheckUserExistenceQuery(
    CheckUserExistenceRequest Payload
    ): IQuery<CheckUserExistenceResponse>, IInjectable;
