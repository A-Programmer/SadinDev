using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Users.GetUserByApiKey;

public record GetUserByApiKeyQuery(GetUserByApiKeyQueryRequest Payload) : IQuery<GetUserByApiKeyQueryResponse>;
