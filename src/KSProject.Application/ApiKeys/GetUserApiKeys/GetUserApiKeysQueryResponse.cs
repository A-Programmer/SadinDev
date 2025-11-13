using System.Text.Json.Serialization;

namespace KSProject.Application.ApiKeys.GetUserApiKeys;

public record GetUserApiKeysQueryResponse(
    [property: JsonPropertyName("apiKeys")] List<ApiKeyDto> ApiKeys
    );
