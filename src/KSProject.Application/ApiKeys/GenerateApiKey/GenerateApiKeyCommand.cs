using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.ApiKeys.GenerateApiKey;

public sealed record GenerateApiKeyCommand(GenerateApiKeyCommandRequest Payload) : ICommand<GenerateApiKeyCommandResponse>;
