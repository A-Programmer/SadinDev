using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.ApiKeys.GetUserApiKeys;

public sealed record GetUserApiKeysQuery(GetUserApiKeysQueryRequest Payload) : IQuery<GetUserApiKeysQueryResponse>;
