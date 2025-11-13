using System.Text.Json.Serialization;

namespace KSProject.Application.ApiKeys.GenerateApiKey;

public record GenerateApiKeyCommandRequest(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("scopes")] string Scopes
    );
