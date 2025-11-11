using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Wallets.ChargeWallet;

public sealed record ChargeWalletCommand(ChargeWalletCommandRequest Payload) : ICommand<ChargeWalletCommandResponse>;
