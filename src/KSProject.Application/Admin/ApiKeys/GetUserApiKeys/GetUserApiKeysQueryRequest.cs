using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.ApiKeys.GetUserApiKeys
{
    public record GetUserApiKeysQueryRequest(
        [property: JsonPropertyName("userId")] Guid UserId
        );
}
