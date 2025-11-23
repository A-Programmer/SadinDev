using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.ApiKeys.RevokeApiKey;

public sealed record RevokeApiKeyCommand(RevokeApiKeyCommandRequest Payload) : ICommand<RevokeApiKeyCommandResponse>;
