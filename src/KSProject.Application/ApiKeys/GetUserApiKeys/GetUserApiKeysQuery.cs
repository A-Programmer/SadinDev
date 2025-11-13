using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.ApiKeys.GetUserApiKeys;

public sealed record GetUserApiKeysQuery(GetUserApiKeysQueryRequest Payload) : IQuery<GetUserApiKeysQueryResponse>;
