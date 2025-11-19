using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Wallets.DebitWallet;

public sealed record DebitWalletCommand(DebitWalletCommandRequest Payload) : ICommand<DebitWalletCommandResponse>;
