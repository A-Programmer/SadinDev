using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

public sealed record GenerateApiKeyCommand(
    GenerateApiKeyCommandRequest Payload) : ICommand<GenerateApiKeyCommandResponse>;
