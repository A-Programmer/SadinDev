using KSFramework.Contracts;
using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Wallets.GetWalletBalance;

public sealed record GetWalletBalanceQuery(Guid UserId) : IQuery<GetWalletBalanceQueryResponse>;
