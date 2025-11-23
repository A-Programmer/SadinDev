using System.Text.Json.Serialization;
using KSProject.Common.Constants.Enums;

namespace KSProject.Application.User.Wallets.DebitWallet;

public sealed record DebitWalletCommandRequest(
    [property: JsonPropertyName("userId")] Guid UserId,
    [property: JsonPropertyName("amount")] decimal Amount,
    [property: JsonPropertyName("metricValue")] decimal MetricValue,
    [property: JsonPropertyName("transactionType")] TransactionTypes TransactionType = TransactionTypes.Usage,
    [property: JsonPropertyName("serviceType")] string ServiceType = null,
    [property: JsonPropertyName("metricType")] string MetricType = null
    
    );
