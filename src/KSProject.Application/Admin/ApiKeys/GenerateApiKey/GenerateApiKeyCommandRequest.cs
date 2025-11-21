using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

public record GenerateApiKeyCommandRequest(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("scopes")] string Scopes
    );
