using System.Text.Json.Serialization;

namespace KSProject.Application.ApiKeys.RevokeApiKey;

public record RevokeApiKeyCommandResponse(
    [property: JsonPropertyName("success")] bool Success
    );
