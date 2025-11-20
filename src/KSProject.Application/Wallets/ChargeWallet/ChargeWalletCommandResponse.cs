using System.Text.Json.Serialization;

namespace KSProject.Application.Wallets.ChargeWallet;

public record ChargeWalletCommandResponse(
    [peoperty: JsonPropertyName("transactionId")] Guid? TransactionId = null
    );
