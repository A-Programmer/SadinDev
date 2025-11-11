using KSFramework.KSMessaging.Abstraction;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace KSProject.Application.Wallets.GetWalletBalance;

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
        Wallet? wallet = await _uow.Wallets.GetByUserIdAsync(request.Payload.UserId, cancellationToken);

        if (wallet is null)
        {
            throw new NotFoundException("Wallet not found for the specified user.");
        }

        return new GetWalletBalanceQueryResponse(wallet.Balance);
    }
}
