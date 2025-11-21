using System.Text.Json.Serialization;

namespace KSProject.Application.User.Users.UserTransactions;

public record UserTransactionListItemResponse(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("amount")] decimal Amount,
    [property: JsonPropertyName("serviceMetric")]  string ServiceMetric,
    [property: JsonPropertyName("metricType")]  string MetricType,
    [property: JsonPropertyName("metricValue")]  decimal MetricValue,
    [property: JsonPropertyName("transactionDate")]  DateTime TransactionDate,
    [property: JsonPropertyName("metricDetails")]  string? MetricDetails,
    [property: JsonPropertyName("transactionStatus")]  string TransactionStatus,
    [property: JsonPropertyName("walletId")] Guid WalletId
    );
