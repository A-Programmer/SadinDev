using System.Text.Json.Serialization;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Wallets.GetWalletBalanceByApiKey;

    public record GetWalletBalanceByApiKeyQuery(
        [property: JsonPropertyName("api_key")] string ApiKey
        ) : IQuery<GetWalletBalanceByApiKeyResponse>;
