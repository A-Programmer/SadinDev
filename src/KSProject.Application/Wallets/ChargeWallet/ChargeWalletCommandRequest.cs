using System.Text.Json.Serialization;
using KSProject.Common.Constants.Enums;

namespace KSProject.Application.Wallets.ChargeWallet;
public record ChargeWalletCommandRequest
(
    [property: JsonPropertyName("amount")] decimal Amount,
    [property: JsonPropertyName("transactionType")] TransactionTypes TransactionType,
    [property: JsonPropertyName("paymentGatewayType")] PaymentGatewayTypes PaymentGatewayType
);
