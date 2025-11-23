using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.User.Wallets.DebitWallet;

public sealed record DebitWalletCommand(DebitWalletCommandRequest Payload) : ICommand<DebitWalletCommandResponse>;
