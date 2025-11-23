using KSFramework.Exceptions;
using KSFramework.KSMessaging.Abstraction;
using KSProject.Application.Admin.Wallets.GetWalletBalanceByApiKey;
using KSProject.Domain.Aggregates.Wallets;
using KSProject.Domain.Contracts;

namespace KSProject.Application.Admin.ApiKeys.GetUserIdByApiKey;

public class GetUserIdByApiKeyQueryHandler :
    IQueryHandler<GetUserIdByApiKeyQuery, GetUserIdByApiKeyResponse>
{
    private readonly IKSProjectUnitOfWork _uow;

    public GetUserIdByApiKeyQueryHandler(IKSProjectUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<GetUserIdByApiKeyResponse> Handle(GetUserIdByApiKeyQuery request,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Users.User user = await _uow.Users.GetUserByApiKey(request.ApiKey);

        if (user is null)
        {
            throw new KSNotFoundException("User not found.");
        }

        return new GetUserIdByApiKeyResponse(user.Id);
    }
}
