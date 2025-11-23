using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.ApiKeys.GenerateApiKey;

public record GenerateApiKeyCommandRequest(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("scopes")] string Scopes,
    [property: JsonPropertyName("domain")] string Domain,
    [property: JsonPropertyName("variant")] string Variant
    );
