using KSFramework.KSMessaging.Abstraction;

namespace KSProject.Application.Wallets.GetWalletBalance;

public sealed record GetWalletBalanceQuery(GetWalletBalanceQueryRequest Payload) : IQuery<GetWalletBalanceQueryResponse>;
