using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.ApiKeys.GetUserApiKeys;

public record GetUserApiKeysQueryResponse(
    [property: JsonPropertyName("apiKeys")] List<ApiKeyDto> ApiKeys
    );
