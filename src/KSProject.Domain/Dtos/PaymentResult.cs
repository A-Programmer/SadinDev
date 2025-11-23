using System.Text.Json.Serialization;

namespace KSProject.Domain.Dtos;

public record PaymentResult(
    [peoperty: JsonPropertyName("success")] bool Success,
    [peoperty: JsonPropertyName("transactionId")] string TransactionId = null,
    [peoperty: JsonPropertyName("redirectUrl")] string RedirectUrl = null,
    [peoperty: JsonPropertyName("errorMessage")] string ErrorMessage = null
    );
