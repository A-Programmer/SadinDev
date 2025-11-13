using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.ApiKeys.RevokeApiKey;

public sealed record RevokeApiKeyCommand(RevokeApiKeyCommandRequest Payload) : ICommand<RevokeApiKeyCommandResponse>;
