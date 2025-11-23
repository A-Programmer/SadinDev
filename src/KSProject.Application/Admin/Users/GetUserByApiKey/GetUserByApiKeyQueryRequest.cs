using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.Users.GetUserByApiKey;

public record GetUserByApiKeyQueryRequest(
    [property: JsonPropertyName("apikey")] string ApiKey
    );
