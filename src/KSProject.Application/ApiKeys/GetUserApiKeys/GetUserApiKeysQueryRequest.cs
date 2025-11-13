using System.Text.Json.Serialization;

namespace KSProject.Application.ApiKeys.GetUserApiKeys
{
    public record GetUserApiKeysQueryRequest(
        [property: JsonPropertyName("userId")] Guid UserId
        );
}
