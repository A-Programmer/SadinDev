using System.Text.Json.Serialization;

namespace KSProject.Application.Admin.Wallets.ChargeWallet;

public record ChargeWalletCommandResponse(
    [peoperty: JsonPropertyName("transactionId")] Guid? TransactionId = null
    );
