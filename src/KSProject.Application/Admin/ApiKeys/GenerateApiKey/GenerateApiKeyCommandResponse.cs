namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

public record GenerateApiKeyCommandResponse(Guid Id, string Key, string Scopes, DateTimeOffset? ExpirationDate);
