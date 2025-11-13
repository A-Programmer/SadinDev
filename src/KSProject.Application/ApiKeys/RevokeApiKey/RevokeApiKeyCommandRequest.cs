using System.Text.Json.Serialization;

namespace KSProject.Application.ApiKeys.RevokeApiKey;

public record RevokeApiKeyCommandRequest(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("apiKeyId")] Guid ApiKeyId
    );
