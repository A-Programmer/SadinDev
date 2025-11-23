using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Wallets.GetWalletBalance;

public sealed class GetWalletBalanceQueryHandler :
    IQueryHandler<GetWalletBalanceQuery, GetWalletBalanceQueryResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetWalletBalanceQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GetWalletBalanceQueryResponse> Handle(GetWalletBalanceQuery request,
        CancellationToken cancellationToken)
    {
        Wallet? wallet = await _uow.Wallets.GetByUserIdAsync(request.UserId, cancellationToken);

        if (wallet is null)
        {
            throw new KSNotFoundException("Wallet not found for the specified user.");
        }

        return new GetWalletBalanceQueryResponse(wallet.Balance);
    }
}
