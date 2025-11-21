using System.Text.Json.Serialization;
using KSProject.Common.Constants.Enums;

namespace KSProject.Application.Admin.Wallets.ChargeWallet;
public record ChargeWalletCommandRequest
(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("amount")] decimal Amount,
    [property: JsonPropertyName("paymentGatewayType")] PaymentGatewayTypes PaymentGatewayType,
    [property: JsonPropertyName("callBackUrl")] string? CallBackUrl
);
