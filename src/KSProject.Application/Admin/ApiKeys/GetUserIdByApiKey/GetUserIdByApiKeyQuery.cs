using System.Text.Json.Serialization;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.ApiKeys.GetUserIdByApiKey;

    public record GetUserIdByApiKeyQuery(
        [property: JsonPropertyName("api_key")] string ApiKey
        ) : IQuery<GetUserIdByApiKeyResponse>;
