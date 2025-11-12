using System.Text.Json.Serialization;

namespace KSProject.Application.Wallets.ChargeWallet;
public record ChargeWalletCommandRequest
(
    [property: JsonPropertyName("amount")] decimal Amount
);
