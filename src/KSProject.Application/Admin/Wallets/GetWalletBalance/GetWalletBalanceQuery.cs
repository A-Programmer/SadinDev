using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Admin.Wallets.GetWalletBalance;

public sealed record GetWalletBalanceQuery(Guid UserId) : IQuery<GetWalletBalanceQueryResponse>;
