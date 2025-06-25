using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Users.CheckUserExistence;

public sealed record CheckUserExistenceQuery(
    CheckUserExistenceRequest Payload
    ): IQuery<CheckUserExistenceResponse>, IInjectable;