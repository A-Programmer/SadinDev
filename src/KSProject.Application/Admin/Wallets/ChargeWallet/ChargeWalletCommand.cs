using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Wallets.ChargeWallet;

public sealed record ChargeWalletCommand(Guid UserId,
    ChargeWalletCommandRequest Payload) : ICommand<ChargeWalletCommandResponse>;
