namespace KSProject.Application.ApiKeys.GenerateApiKey;

public record GenerateApiKeyCommandResponse(Guid Id, string Key, string Scopes, DateTimeOffset? ExpirationDate);
