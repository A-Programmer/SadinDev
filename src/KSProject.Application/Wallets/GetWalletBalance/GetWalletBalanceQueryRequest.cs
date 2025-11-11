using KSFramework.Contracts;

namespace KSProject.Application.Wallets.GetWalletBalance;
public sealed class GetWalletBalanceQueryRequest : IInjectable
{
    public required Guid UserId { get; set; }
}
