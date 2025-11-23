using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.Wallets.GetWalletBalance;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.Wallets.GetWalletBalanceByApiKey;

public class GetWalletBalanceByApiKeyQueryHandler :
    IQueryHandler<GetWalletBalanceByApiKeyQuery, GetWalletBalanceByApiKeyResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetWalletBalanceByApiKeyQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GetWalletBalanceByApiKeyResponse> Handle(GetWalletBalanceByApiKeyQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Users.User user = await _uow.Users.GetUserByApiKey(request.ApiKey);
        Wallet? wallet = await _uow.Wallets.GetByUserIdAsync(user.Id, cancellationToken);

        if (wallet is null)
        {
            throw new KSNotFoundException("Wallet not found for the specified user.");
        }

        return new GetWalletBalanceByApiKeyResponse(wallet.Balance);
    }
}
