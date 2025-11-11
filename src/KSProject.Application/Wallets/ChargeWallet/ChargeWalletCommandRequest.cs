using KSFramework.Contracts;

namespace KSProject.Application.Wallets.ChargeWallet;
public sealed class ChargeWalletCommandRequest : IInjectable
{
    public required Guid UserId { get; set; }
    public required decimal Amount { get; set; }
}
